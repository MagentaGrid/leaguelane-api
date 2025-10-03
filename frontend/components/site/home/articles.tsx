import { getArticles } from "@/api/articlesApi"
import { ArticleCards } from "./article-cards"

export async function Articles() {
  const articles = await getArticles()

  return <ArticleCards articles={articles} />
}
