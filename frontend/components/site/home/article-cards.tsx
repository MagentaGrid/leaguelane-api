import Image from "next/image"
import { Button } from "@/components/ui/button"
import { FileText } from "lucide-react"
import type { Article } from "@/types/article"

interface ArticleCardsProps {
  articles: Article[]
}

export function ArticleCards({ articles }: ArticleCardsProps) {
  return (
    <div className="space-y-4">
      <h3 className="font-bold text-xl flex items-center gap-2 text-primary">
        <FileText className="h-5 w-5" />
        Articles
      </h3>

      {articles.map((article) => (
        <div key={article.id} className="bg-card rounded-xl shadow-sm overflow-hidden">
          <Image
            alt={article.title}
            className="w-full h-48 object-cover"
            src={article.image || "/placeholder.svg?height=192&width=400"}
            width={400}
            height={192}
          />
          <div className="p-4">
            <h4 className="font-bold text-lg mb-2 text-foreground">{article.title}</h4>
            <p className="text-sm text-muted-foreground">{article.date}</p>
          </div>
        </div>
      ))}

      <div className="text-center">
        <Button
          variant="outline"
          className="py-2 px-6 bg-primary/10 text-primary rounded-lg font-semibold hover:bg-primary/20 transition-colors"
        >
          View All Articles
        </Button>
      </div>
    </div>
  )
}
