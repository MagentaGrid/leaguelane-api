export interface Offer {
  id: number
  header: string
  description: string
  url: string
}

export type OfferResponse = {
  isSuccess: boolean
  errorMessage?: string | null
  Offers: Offer[]
}
