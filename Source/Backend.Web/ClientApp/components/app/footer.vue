<script>
  export default {
    methods: {
      scrollTop() {
        $("html, body").animate({ scrollTop: 0 }, "slow");
      }
    }
  };
</script>

<template>
  <footer id="app-footer">
    <div class="col d-flex align-items-center text-dark">
      <span class="font-weight-bold">&copy;&nbsp;2019 Ferrari Soluções.</span>
      <span class="d-none d-md-inline">&nbsp;Todos os direitos reservados.</span>
    </div>
    <a href="#/" @click="scrollTop()">
      <i class="fas fa-fw fa-arrow-up" />
    </a>
  </footer>
</template>
