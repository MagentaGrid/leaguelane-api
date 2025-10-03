import { getHighlights } from "@/api/highlightsApi"

export async function Highlights() {
  const highlights = await getHighlights()

  return (
    <section className="space-y-8">
      {highlights.map((highlight) => (
        <div key={highlight.id} className="text-center space-y-4">
          <h2 className="text-2xl font-bold text-foreground">{highlight.title}</h2>
          <p className="text-muted-foreground leading-relaxed max-w-4xl mx-auto">{highlight.content}</p>
        </div>
      ))}
    </section>
  )
}
