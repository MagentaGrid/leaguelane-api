"use client"

import * as React from "react";
import { useActionState } from 'react';
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { authenticate, type FormState, type Errors } from "@/app/action/login/login-action";
import { Mail, Lock } from "lucide-react";
import Image from "next/image";

export default function LoginForm({ className, ...props }: React.ComponentProps<"div">) { // Changed to "div" since it wraps more now

    const initialState: FormState = { errors: {} as Errors };
    const [state, formAction, isPending] = useActionState(authenticate, initialState);

    return (
        <div className={cn("grid min-h-screen grid-cols-1 lg:grid-cols-2", className)} {...props}>
            {/* Left Side - Login Form */}
            <div className="flex flex-col justify-center px-8 md:px-16 lg:px-24 xl:px-32 bg-white">
                <div className="mb-12">
                    <div className="flex items-center gap-2">
                        <Image src="/images/ll-logo.png" alt="League Lane Logo" width={200} height={50} className="h-12 w-auto object-contain" />
                    </div>
                </div>

                <div className="mb-8">
                    <h1 className="text-3xl md:text-4xl font-bold text-gray-900 leading-tight">
                        Welcome to <br />
                        LEAGUE LANE <br />
                        Admin Panel.
                    </h1>
                    <p className="mt-4 text-gray-400 text-sm">
                        Enter your details to proceed further
                    </p>
                </div>

                <div className="w-full max-w-sm">
                    <form action={formAction} className="flex flex-col gap-6">
                        <div className="grid gap-6">
                            <div className="grid gap-2">
                                <Label htmlFor="email" className="text-gray-400 font-normal">Email</Label>
                                <div className="relative">
                                    <Mail className="absolute left-3 top-2.5 h-4 w-4 text-gray-400" />
                                    <Input
                                        id="email"
                                        name="email"
                                        type="email"
                                        placeholder="Enter your email"
                                        required
                                        className="pl-10 border-gray-200 h-10"
                                    />
                                </div>
                                {state.errors.email && <p className="text-sm text-red-600">{state.errors.email}</p>}
                            </div>

                            <div className="grid gap-2">
                                <Label htmlFor="password" className="text-gray-400 font-normal">Password</Label>
                                <div className="relative">
                                    <Lock className="absolute left-3 top-2.5 h-4 w-4 text-gray-400" />
                                    <Input
                                        id="password"
                                        name="password"
                                        type="password"
                                        placeholder="Enter your Password"
                                        required
                                        className="pl-10 border-gray-200 h-10"
                                    />
                                </div>
                                {state.errors.password && <p className="text-sm text-red-600">{state.errors.password}</p>}
                                <div className="flex justify-end">
                                    <a
                                        href="#"
                                        className="text-sm font-medium text-[#2BADF8] hover:underline"
                                    >
                                        Forgot password
                                    </a>
                                </div>
                            </div>

                            {state.errors.form && (
                                <p className="text-sm text-red-600" role="alert">{state.errors.form}</p>
                            )}

                            <Button
                                type="submit"
                                className="w-full bg-[#2BADF8] hover:bg-[#1a9de0] text-white font-medium h-10"
                                disabled={isPending}
                            >
                                {isPending ? "Signing In..." : "Sign In"}
                            </Button>
                        </div>
                    </form>
                </div>
            </div>

            {/* Right Side - Hero Image */}
            <div className="relative hidden bg-[#3b82f6] lg:flex items-center justify-center">
                <div className="relative w-full h-full flex items-center justify-center p-20">
                    <Image
                        src="/images/Login/loginHero.png"
                        alt="Login Hero"
                        width={800}
                        height={800}
                        className="object-contain max-h-full max-w-full"
                        priority
                    />
                </div>
            </div>
        </div>
    )
}

