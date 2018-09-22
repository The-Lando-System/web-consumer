<template>
  <div id="app">
    <web-request/>
  </div>
</template>

<script>
import WebRequest from './components/WebRequest.vue'

export default {
  name: 'app',
  components: {
    WebRequest
  }
}
</script>

<style>
  #app {
    margin: 20px;
  }
</style>
