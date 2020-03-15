<script>
  import AppFooter from "./footer";
  import AppHeader from "./header";
  import AppMessage from "./message";
  import AppOverlay from "./overlay";
  import AppSidebar from "./sidebar";
  import AppStatus from "./status";

  export default {
    components: {
      appFooter: AppFooter,
      appHeader: AppHeader,
      appMessage: AppMessage,
      appOverlay: AppOverlay,
      appSidebar: AppSidebar,
      appStatus: AppStatus
    },
    data() {
      return {
        innerWidth: 0,
        sidebarToggled: false,
        filtersToggled: false
      };
    },
    mounted() {
      let that = this;
      this.$nextTick(() => {
        window.addEventListener("resize", () => {
          that.innerWidth = that.innerWidth || window.innerWidth;
          if (this.timer) clearTimeout(this.timer);
          this.timer = setTimeout(function() {
            if (
              (that.innerWidth < 768 && window.innerWidth >= 768) || //
              (that.innerWidth > 768 && window.innerWidth <= 768) ||
              (that.innerWidth < 1200 && window.innerWidth >= 1200) ||
              (that.innerWidth > 1200 && window.innerWidth <= 1200)
            ) {
              that.sidebarToggled = that.filtersToggled = false;
            }
            that.innerWidth = 0;
          }, 300);
        });
      });
    }
  };
</script>

<template>
  <main id="app" :class="{ 'sidebar-toggled': sidebarToggled, 'filters-toggled': filtersToggled }">
    <keep-alive>
      <app-sidebar v-once />
    </keep-alive>
    <section id="app-content">
      <keep-alive>
        <app-header v-once />
      </keep-alive>
      <router-view />
      <keep-alive>
        <app-footer v-once />
      </keep-alive>
    </section>
    <keep-alive>
      <app-status v-once />
    </keep-alive>
    <keep-alive>
      <app-message v-once />
    </keep-alive>
    <keep-alive>
      <app-overlay v-once />
    </keep-alive>
  </main>
</template>
