"use client"

import { HeaderBar } from "./header-bar"
import { SportsCategories } from "./sports-categories"

export function Navigation() {
  return (
    <nav className="w-full bg-background border-b border-border">
      <HeaderBar />
      <SportsCategories />
    </nav>
  )
}
