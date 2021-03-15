export function throwIf(condition: boolean, error) {
  if (condition) {
    throw error
  }
}

export function throwUnless(condition: boolean, error) {
  throwIf(!condition, error)
}
