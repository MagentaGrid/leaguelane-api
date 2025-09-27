"use client"

import { Search, Menu } from "lucide-react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import Image from "next/image"

export function HeaderBar() {
  return (
    <div className="flex items-center justify-between px-4 py-3 max-w-7xl mx-auto">
      {/* Logo */}
      <div className="flex items-center">
        <Image src="/images/ll-logo.png" alt="LeagueLane" width={200} height={40} className="h-8 w-auto" priority />
      </div>

      {/* Search Bar */}
      <div className="flex-1 max-w-md mx-8">
        <div className="relative">
          <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-muted-foreground h-4 w-4" />
          <Input
            type="search"
            placeholder="Search"
            className="pl-10 bg-muted/50 border-border focus:border-primary focus:ring-primary/20"
          />
        </div>
      </div>

      {/* Mobile Menu Button */}
      <Button variant="outline" size="icon" className="md:hidden bg-transparent">
        <Menu className="h-4 w-4" />
      </Button>

      {/* Desktop Menu Button */}
      <Button variant="outline" size="icon" className="hidden md:flex bg-transparent">
        <Menu className="h-4 w-4" />
      </Button>
    </div>
  )
}
