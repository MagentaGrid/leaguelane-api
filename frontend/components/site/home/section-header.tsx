import type React from "react"
interface SectionHeaderProps {
  title: string
  icon?: React.ReactNode
}

export function SectionHeader({ title, icon }: SectionHeaderProps) {
  return (
    <div className="bg-primary/10 rounded-lg px-4 py-3 mb-6">
      <div className="flex items-center gap-3">
        {icon && (
          <div className="w-8 h-8 bg-primary rounded-lg flex items-center justify-center text-primary-foreground">
            {icon}
          </div>
        )}
        <h2 className="text-xl font-semibold text-foreground">{title}</h2>
      </div>
    </div>
  )
}
