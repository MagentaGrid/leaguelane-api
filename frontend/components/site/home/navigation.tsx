"use client"

import { HeaderBar } from "./header-bar"
import { SportsCategories } from "./sports-categories"

export function Navigation() {
  return (
    <nav className="w-full bg-background">
      <HeaderBar />
      <SportsCategories />
    </nav>
  )
}
