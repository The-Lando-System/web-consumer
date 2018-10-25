<template>
  <div class="results">
    <pre>{{responseMessage}}</pre>
  </div>
</template>

<script>
export default {
  props: ['responseMessage']
}
</script>

<style>
.results {
  margin-top: 10px;
  margin-bottom: 10px;
  text-align: left;
  padding: 20px;
  border: solid 0.5px #e2e1e0;
  border-radius: 2px;
}
</style>
