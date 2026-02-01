<script setup lang="ts">
import { ref } from 'vue'

// --- 状态逻辑 ---
// 控制 Cookie 条的显示与隐藏
const showCookieConsent = ref(true)

const acceptCookies = () => {
  showCookieConsent.value = false
  // 这里可以加入保存 cookie 同意状态到 localStorage 的逻辑
  // localStorage.setItem('cookie_consent', 'true')
}

// 动态获取年份
const currentYear = new Date().getFullYear()

// --- 模拟数据 (图片路径请替换为真实项目路径) ---
// 建议将图片放在 /public/images/ 目录下
const paymentIcons = [
  { name: 'Visa', src: 'https://placehold.co/60x36/EEE/31343C?text=VISA' },
  { name: 'Mastercard', src: 'https://placehold.co/60x36/EEE/31343C?text=MC' },
  { name: 'Amex', src: 'https://placehold.co/60x36/EEE/31343C?text=AMEX' },
]

const partnerLogos = [
  { name: 'ABTA', src: 'https://placehold.co/80x40/transparent/333?text=ABTA' },
  { name: 'ATOL', src: 'https://placehold.co/40x40/transparent/333?text=ATOL' },
  { name: 'IATA', src: 'https://placehold.co/60x40/transparent/333?text=IATA' },
  { name: 'ATAS', src: 'https://placehold.co/60x40/transparent/333?text=ATAS' },
  { name: 'LATA', src: 'https://placehold.co/60x40/transparent/333?text=LATA' },
]
</script>

<template>
  <footer class="w-full bg-white font-sans text-gray-600 pt-10">
    
    <!-- Top Divider Line -->
    <div class="max-w-6xl mx-auto px-4">
      <hr class="border-t border-gray-200 mb-10" />
    </div>

    <div class="max-w-4xl mx-auto px-4 text-center flex flex-col items-center gap-10 pb-12">
      
      <!-- 1. Payment Protection Section -->
      <section>
        <h3 class="text-2xl text-gray-700 mb-6 font-light">Your Payment is Protected</h3>
        <div class="flex justify-center gap-4">
          <img 
            v-for="icon in paymentIcons" 
            :key="icon.name" 
            :src="icon.src" 
            :alt="icon.name" 
            class="h-9 w-auto object-contain" 
          />
        </div>
      </section>

      <!-- 2. Travel Aware Section -->
      <section class="max-w-3xl">
        <h3 class="text-2xl text-gray-700 mb-6 font-light">Travel Aware - Staying Safe and Healthy Abroad</h3>
        <p class="text-sm leading-relaxed mb-2 text-gray-600">
          The Foreign & Commonwealth Office and National Travel Health Network and Centre have up-to-date advice on staying safe and healthy abroad. For the latest travel advice from the Foreign & Commonwealth Office including security and local laws, plus passport and visa information check 
          <a href="#" class="underline decoration-1 hover:text-gray-900 transition-colors">gov.uk/foreign-travel-advice</a>
        </p>
      </section>

      <!-- 3. Accreditation Logos -->
      <section class="flex flex-wrap justify-center items-center gap-8 md:gap-12 mt-4 grayscale opacity-80 hover:grayscale-0 hover:opacity-100 transition-all duration-300">
        <img 
          v-for="logo in partnerLogos" 
          :key="logo.name" 
          :src="logo.src" 
          :alt="logo.name"
          class="h-10 w-auto object-contain"
        />
      </section>

      <!-- 4. Copyright -->
      <div class="text-xs text-gray-500 mt-2">
        ©{{ currentYear }} Wendy Wu Tours, All Rights Reserved. ABTA: W7994, ATOL: 6639 and IATA.
      </div>

    </div>

    <!-- 5. Bottom Dark Bar (Company Info) -->
    <div class="w-full bg-[#1a1a1a] text-[#555] py-6 text-center text-sm">
      <p>Company Registration No: 5107061</p>
    </div>

    <!-- 6. Cookie Consent Bar (Fixed at Bottom) -->
    <!-- 
      z-50: 确保在最上层
      bg-[#111]/95: 深色背景带一点透明度
    -->
    <transition
      enter-active-class="transition ease-out duration-300"
      enter-from-class="translate-y-full"
      enter-to-class="translate-y-0"
      leave-active-class="transition ease-in duration-200"
      leave-from-class="translate-y-0"
      leave-to-class="translate-y-full"
    >
      <div 
        v-if="showCookieConsent"
        class="fixed bottom-0 left-0 right-0 z-50 bg-[#111]/95 text-white py-4 px-6 shadow-[0_-4px_6px_-1px_rgba(0,0,0,0.1)] backdrop-blur-sm"
      >
        <div class="max-w-7xl mx-auto flex flex-col sm:flex-row items-center justify-center gap-4 text-center sm:text-left">
          <p class="text-sm font-light">
            Our website uses cookies to deliver you the best possible web experience. 
            <a href="#" class="underline hover:text-gray-300">Learn more</a>
          </p>
          <button 
            @click="acceptCookies"
            class="bg-[#ff5e45] hover:bg-[#ff452b] text-white text-xs font-bold py-2 px-6 rounded uppercase tracking-wide transition-colors shadow-lg min-w-[100px]"
          >
            Got It!
          </button>
        </div>
      </div>
    </transition>

  </footer>
</template>

<style scoped>
/* 可选：如果你需要对图片做更细致的灰度控制 */
img {
  max-width: 100%;
}
</style>