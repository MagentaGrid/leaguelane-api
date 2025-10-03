import { getOffers } from "@/api/offerApi"
import { OfferCards } from "./offer-cards"

export async function Offers() {
  const offers = await getOffers()

  return <OfferCards offers={offers} />
}
