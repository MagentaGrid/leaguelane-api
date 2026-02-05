"use server";

import { redirect } from "next/navigation";
import { cookies } from "next/headers";
import { apiClient } from "@/lib/api-client";

export type Errors = {
    email?: string;
    password?: string;
    form?: string;
};

export type FormState = {
    errors: Errors;
};

type LoginResponse = {
    isSuccess: boolean;
    errorMessage?: string;
    token?: string;
    user?: {
        userName: string;
        firstName: string;
        lastName: string;
        role: number;
    };
};

export async function authenticate(_prevState: FormState, formData: FormData): Promise<FormState> {
    const email = (formData.get("email") ?? "").toString().trim();
    const password = (formData.get("password") ?? "").toString();

    const errors: Errors = {};
    if (!email) errors.email = "Email is required.";
    if (!password) errors.password = "Password is required.";
    if (Object.keys(errors).length) return { errors };

    try {
        const response = await apiClient<LoginResponse>("users/authenticate", {
            method: "POST",
            body: { userName: email, password: password },
        });

        if (response.isSuccess && response.token) {
            // Store token in secure HttpOnly cookie
            const cookieStore = await cookies();
            cookieStore.set("auth_token", response.token, {
                httpOnly: true,
                secure: process.env.NODE_ENV === "production",
                sameSite: "strict",
                path: "/",
                maxAge: 60 * 60 * 24 * 7, // 1 week
            });

        } else {
            return { errors: { form: response.errorMessage || "Login failed" } };
        }

    } catch (error: any) {
        console.error("Login error:", error);
        return { errors: { form: error.message || "An unexpected error occurred." } };
    }

    redirect("/admin");
}
