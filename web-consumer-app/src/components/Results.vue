<template>
  <div class="results">
    <div class="result-status-pane">
      <div class="row">
        <div class="col-sm-4">
          <span><b>Status: </b></span>
          <span v-bind:class="responseStatus">{{response.Status || "N/A"}}</span>
        </div>
        <div class="col-sm-8">
          <span><b>Content Type: </b></span>
          <span>{{response.ContentType || "N/A"}}</span>
        </div>
      </div>
    </div>
    <div class="result-data-pane">
      <pre>{{response.Data}}</pre>
    </div>
  </div>
</template>

<script>
export default {
  props: ['response'],
  computed: {
    responseStatus: function () {
      return {
        'ok': this.response.Status === 'OK',
        'warn': this.response.Status === 'BadRequest'
      }
    }
  }
}
</script>

<style>
.ok {
  color: #87bc87;
}

.warn {
  color: #e1bb49;
}

.results {
  margin-top: 10px;
  margin-bottom: 10px;
}

.result-status-pane {
  text-align: left;
  padding: 20px;
  border: solid 0.5px #e2e1e0;
  border-radius: 2px;
  margin-bottom: 10px;
}

.result-data-pane {
  text-align: left;
  padding: 20px;
  border: solid 0.5px #e2e1e0;
  border-radius: 2px;
}


</style>
