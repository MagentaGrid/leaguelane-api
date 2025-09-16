import { Navigation } from "@/components/navigation"
import { TopLeagues } from "@/components/top-leagues"
import { PromotionalCards } from "@/components/promotional-cards"

export default function Home() {
  return (
    <div className="min-h-screen">
      <nav className="w-full bg-background border-b border-border">
        <Navigation />
      </nav>

      <main className="min-h-screen" style={{ backgroundColor: "#F2F2F2" }}>
        <div className="max-w-7xl mx-auto px-4 py-8">
          <div className="flex gap-6">
            <TopLeagues />

            {/* Main Content Area */}
            <div className="flex-1">
              <PromotionalCards />
            </div>
          </div>
        </div>
      </main>
    </div>
  )
}
