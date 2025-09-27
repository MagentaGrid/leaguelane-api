"use server";

import { redirect } from "next/navigation";

export type Errors = {
    email?: string;
    password?: string;
    form?: string;
};

export type FormState = {
    errors: Errors;
};

async function verifyCredentials(email: string, password: string): Promise<boolean> {
    debugger;
    return email === "admin@example.com" && password === "secret123";
}

export async function authenticate(_prevState: FormState, formData: FormData): Promise<FormState>
{

    const email = (formData.get("email") ?? "").toString().trim();
    const password = (formData.get("password") ?? "").toString();

    console.log("emil=" + email);

    const errors: Errors = {};
    if (!email) errors.email = "Email is required.";
    if (!password) errors.password = "Password is required.";
    if (Object.keys(errors).length) return { errors };

    const ok = await verifyCredentials(email, password);
    if (!ok) return { errors: { form: "Invalid email or password." } };

    redirect("/admin");
}
