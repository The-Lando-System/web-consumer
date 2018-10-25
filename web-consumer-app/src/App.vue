<template>
  <div id="app">
    <div class="container-fluid">
      <div class="jumbotron jumbotron-fluid">
        <div class="container">
          <h1 class="display-4">Web Consumer</h1>
          <p class="lead">Submit web requests to various APIs and save them for later...</p>
        </div>
      </div>
      <web-request/>
    </div>
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
