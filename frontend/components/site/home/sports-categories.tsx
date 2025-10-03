"use client"

import { cn } from "@/lib/utils"
import { useState } from "react"
import { MoreHorizontal, CircleDot, Rocket as Racquet, Circle, Disc3, Hand, Egg, Target } from "lucide-react"

const sportsCategories = [
  { name: "Football", icon: CircleDot, active: true },
  { name: "Tennis", icon: Racquet },
  { name: "Basketball", icon: Target },
  { name: "Ice Hockey", icon: Disc3 },
  { name: "Handball", icon: Hand },
  { name: "American Football", icon: Egg },
  { name: "Baseball", icon: Circle },
]

export function SportsCategories() {
  const [activeCategory, setActiveCategory] = useState("Football")

  return (
    <nav className="mb-8">
      <div className="max-w-7xl mx-auto px-4">
        <ul className="flex flex-wrap items-center justify-center gap-2 md:gap-4 bg-card p-2 rounded-lg shadow-sm">
          {sportsCategories.map((category) => {
            const isActive = activeCategory === category.name
            const IconComponent = category.icon

            return (
              <li key={category.name}>
                <button
                  onClick={() => setActiveCategory(category.name)}
                  className={cn(
                    "flex items-center gap-2 px-4 py-2 rounded-full font-medium transition-colors relative",
                    isActive
                      ? "text-primary font-semibold"
                      : "hover:bg-muted text-muted-foreground hover:text-foreground",
                  )}
                >
                  <IconComponent
                    className={cn("h-4 w-4", category.name === "Football" && isActive ? "text-primary" : "")}
                  />
                  {category.name}
                  {isActive && <div className="absolute bottom-0 left-0 right-0 h-1 bg-primary rounded-sm" />}
                </button>
              </li>
            )
          })}
          <li>
            <button className="flex items-center gap-2 px-4 py-2 rounded-full font-medium transition-colors text-muted-foreground hover:text-foreground hover:bg-muted">
              <MoreHorizontal className="h-4 w-4" />
              More
            </button>
          </li>
        </ul>
      </div>
    </nav>
  )
}
