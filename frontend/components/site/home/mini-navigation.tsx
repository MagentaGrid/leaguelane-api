"use client"

import { cn } from "@/lib/utils"
import { useState } from "react"

const miniNavItems = [
  { name: "Predictions", active: true }, // moved Predictions to first position
  { name: "Leagues", active: false }, // moved Leagues to second position
  { name: "Football Tips", active: false }, // Reverted "Tips" back to "Football Tips"
  { name: "Bet of the Day", active: false },
]

export function MiniNavigation() {
  const [activeItem, setActiveItem] = useState("Predictions")

  return (
    <nav className="hidden md:flex items-center gap-1">
      {miniNavItems.map((item) => {
        const isActive = activeItem === item.name

        return (
          <button
            key={item.name}
            onClick={() => setActiveItem(item.name)}
            className={cn(
              "px-4 py-2 rounded-full text-sm font-medium transition-colors",
              isActive
                ? "bg-primary text-primary-foreground"
                : "text-muted-foreground hover:text-foreground hover:bg-muted",
            )}
          >
            {item.name}
          </button>
        )
      })}
    </nav>
  )
}
