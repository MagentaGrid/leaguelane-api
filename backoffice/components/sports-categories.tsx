"use client"

import { PhoneCall as Football, Trophy, Target, Zap, Hand, Users, Gamepad2 } from "lucide-react"
import { cn } from "@/lib/utils"
import { useState } from "react"

const sportsCategories = [
  { name: "Football", icon: Football, active: true },
  { name: "Tennis", icon: Target },
  { name: "Basketball", icon: Trophy },
  { name: "Ice Hockey", icon: Zap },
  { name: "Handball", icon: Hand },
  { name: "American Football", icon: Users },
  { name: "Baseball", icon: Gamepad2 },
]

export function SportsCategories() {
  const [activeCategory, setActiveCategory] = useState("Football")

  return (
    <div className="border-t border-border bg-muted/30">
      <div className="flex items-center px-4 max-w-7xl mx-auto overflow-x-auto">
        {sportsCategories.map((category) => {
          const IconComponent = category.icon
          const isActive = activeCategory === category.name

          return (
            <button
              key={category.name}
              onClick={() => setActiveCategory(category.name)}
              className={cn(
                "flex items-center gap-2 px-4 py-3 text-sm font-medium whitespace-nowrap transition-colors hover:bg-accent hover:text-accent-foreground",
                isActive ? "text-white" : "text-muted-foreground hover:text-foreground",
              )}
              style={isActive ? { backgroundColor: "#17A4DD" } : {}}
            >
              <IconComponent className="h-4 w-4" />
              {category.name}
            </button>
          )
        })}
      </div>
    </div>
  )
}
