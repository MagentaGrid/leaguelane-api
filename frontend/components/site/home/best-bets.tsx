import { getBestBets } from "@/api/bestBetsApi"
import { BestBetCards } from "./best-bet-cards"

export async function BestBets() {
  const bestBets = await getBestBets()

  return <BestBetCards bestBets={bestBets} />
}
