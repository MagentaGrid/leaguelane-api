import Image from "next/image"
import { Button } from "@/components/ui/button"
import type { Offer } from "@/types/offer"

interface OfferCardsProps {
  offers: Offer[]
}

export function OfferCards({ offers }: OfferCardsProps) {
  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 mb-8">
      {offers.map((offer) => (
        <div
          key={offer.id}
          className="bg-card p-6 rounded-xl shadow-sm border border-border flex flex-col items-center text-center"
        >
          <Image
            alt={offer.header}
            className="h-16 mb-4 rounded-lg object-cover"
            src="/placeholder.svg?height=64&width=200"
            width={200}
            height={64}
          />
          <h4 className="text-lg font-bold text-foreground">{offer.header}</h4>
          <p className="text-sm text-muted-foreground mb-4">{offer.description}</p>
          <Button className="w-full py-2 px-4 bg-primary text-primary-foreground rounded-full font-semibold hover:bg-primary/90 transition-colors">
            Claim Now
          </Button>
        </div>
      ))}
    </div>
  )
}
