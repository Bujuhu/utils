export function clearAllChildren(el = []) {
    const elements = Array.from(el.children)
    elements.forEach(el => el.remove())
}