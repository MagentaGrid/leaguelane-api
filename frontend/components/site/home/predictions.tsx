import { getPredictions } from "@/api/predictionsApi"
import { PredictionCards } from "./prediction-cards"

export async function Predictions() {
  const predictions = await getPredictions()

  return <PredictionCards predictions={predictions} />
}
