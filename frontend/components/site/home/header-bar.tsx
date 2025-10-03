"use client"

import { Search } from "lucide-react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { MiniNavigation } from "./mini-navigation"
import Image from "next/image"

export function HeaderBar() {
  return (
    <div className="flex flex-col lg:flex-row justify-between items-center px-4 py-6 max-w-7xl mx-auto gap-4">
      {/* Logo */}
      <div className="flex items-center space-x-2">
        <Image src="/images/soccer-logo.png" alt="LeagueLane Logo" width={32} height={32} className="h-8 w-8" />
        <h1 className="text-2xl font-bold text-foreground">LEAGUELANE</h1>
      </div>

      <MiniNavigation />

      {/* Search Bar and Sign Up */}
      <div className="flex items-center gap-3">
        <div className="relative">
          <Input
            type="search"
            placeholder="Search"
            className="w-64 pl-10 pr-4 py-2 rounded-lg bg-card border border-border focus:outline-none focus:ring-2 focus:ring-primary"
          />
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 text-muted-foreground h-4 w-4" />
        </div>
        <Button className="px-4 py-2 bg-primary text-primary-foreground rounded-full font-semibold hover:bg-primary/90 transition-colors">
          Sign Up
        </Button>
      </div>
    </div>
  )
}
