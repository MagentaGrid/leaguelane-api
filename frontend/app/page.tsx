import { Navigation } from "@/components/site/home/navigation"
import { TopLeagues } from "@/components/site/home/top-leagues"
import { Offers } from "@/components/site/home/offers"
import { Predictions } from "@/components/site/home/predictions"
import { BestBets } from "@/components/site/home/best-bets"
import { Articles } from "@/components/site/home/articles"
import { Highlights } from "@/components/site/home/highlights"
import { Footer } from "@/components/site/home/footer"

export default function Home() {
  return (
    <div className="min-h-screen bg-background">
      <header className="w-full bg-background">
        <Navigation />
      </header>

      <main className="container mx-auto px-4 py-8">
        <div className="grid grid-cols-1 lg:grid-cols-4 gap-8">
          {/* Sidebar */}
          <TopLeagues />

          {/* Main Content Area */}
          <div className="lg:col-span-3 space-y-8">
            <Offers />
            <Predictions />

            <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
              <BestBets />
              <Articles />
            </div>

            <Highlights />
          </div>
        </div>
      </main>

      <Footer />
    </div>
  )
}
