export interface Article {
  id: number
  title: string
  date: string
  image: string
  url: string
  excerpt?: string
}

export type ArticleResponse = {
  isSuccess: boolean
  errorMessage?: string | null
  Articles: Article[]
}
