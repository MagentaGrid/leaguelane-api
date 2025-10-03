export interface BestBet {
  id: number
  title: string
  date: string
  image: string
  url: string
}

export type BestBetResponse = {
  isSuccess: boolean
  errorMessage?: string | null
  BestBets: BestBet[]
}
