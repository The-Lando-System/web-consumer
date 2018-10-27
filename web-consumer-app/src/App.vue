<template>
  <div id="app">
    <div class="container-fluid">

      <div class="jumbotron jumbotron-fluid">
        <div class="container">
          <h1 class="display-4">Web Consumer</h1>
          <p class="lead">Submit web requests to various APIs and save them for later...</p>
        </div>
      </div>

      <div class="row">

        <div class="col-md-6">
          <request-area />
        </div>
        <div class="col-md-6">
          <results-area />
        </div>
        
      </div>

    </div>
  </div>
</template>

<script>
import RequestArea from './components/RequestArea.vue'
import ResultsArea from './components/ResultsArea.vue';

export default {
  name: 'app',
  components: {
    RequestArea,
    ResultsArea
  }
}
</script>

<style>
  #app {
    margin: 20px;
  }
</style>
