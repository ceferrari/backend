<template>
  <header id="app-header" class="sticky-top">
    <a href="#/" @click="$parent.sidebarToggled = !$parent.sidebarToggled">
      <i class="fas fa-fw fa-bars" />
    </a>
    <a href="#/" class="flex-fill text-center">{{ $moment().format("llll") }}</a>
    <a href="#/" @click="$parent.filtersToggled = !$parent.filtersToggled">
      <i class="fas fa-fw fa-filter" />
    </a>
  </header>
</template>
