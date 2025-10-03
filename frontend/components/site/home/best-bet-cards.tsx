import Image from "next/image"
import { Button } from "@/components/ui/button"
import { TrendingUp } from "lucide-react"
import type { BestBet } from "@/types/bestBet"

interface BestBetCardsProps {
  bestBets: BestBet[]
}

export function BestBetCards({ bestBets }: BestBetCardsProps) {
  return (
    <div className="space-y-4">
      <h3 className="font-bold text-xl flex items-center gap-2 text-primary">
        <TrendingUp className="h-5 w-5" />
        Best Bets
      </h3>

      {bestBets.map((bet) => (
        <div key={bet.id} className="bg-card rounded-xl shadow-sm overflow-hidden">
          <Image
            alt={bet.title}
            className="w-full h-48 object-cover"
            src={bet.image || "/placeholder.svg?height=192&width=400"}
            width={400}
            height={192}
          />
          <div className="p-4">
            <h4 className="font-bold text-lg mb-2 text-foreground">{bet.title}</h4>
            <p className="text-sm text-muted-foreground">{bet.date}</p>
          </div>
        </div>
      ))}

      <div className="text-center">
        <Button
          variant="outline"
          className="py-2 px-6 bg-primary/10 text-primary rounded-lg font-semibold hover:bg-primary/20 transition-colors"
        >
          View All Best Bets
        </Button>
      </div>
    </div>
  )
}
