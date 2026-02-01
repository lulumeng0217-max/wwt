import { defineAsyncComponent } from 'vue'

const ALLOW_HOSTS = [
  'localhost',
  '127.0.0.1'
]

export function loadRemoteComponent(url: string) {
  const { host } = new URL(url, location.origin)

  if (!ALLOW_HOSTS.includes(host)) {
    throw new Error(`Remote host not allowed: ${host}`)
  }

  return defineAsyncComponent(async () => {
    const mod = await import(/* @vite-ignore */ url)
    return mod.default
  })
}
