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
    <div ref="resultsPane" class="result-data-pane">
      <pre>{{response.Data}}</pre>
      <iframe id="results-iframe"></iframe>
    </div>
  </div>
</template>

<script>
export default {
  data: function() {
    return {
      response: {},
      isHtml: false
    }
  },
  computed: {
    responseStatus: function () {
      return {
        'ok': this.response.Status === 'OK',
        'warn': this.response.Status === 'BadRequest'
             || this.response.Status === 'NotFound'
      }
    }
  },
  mounted: function() {
    this.$broadcaster.on('executedRequest', (data) => {
      this.response = data;

      var iframe = this.$refs.resultsPane.querySelector("#results-iframe");
      
      if (this.response.ContentType.includes('text/html')) {
        
        var iframedoc = iframe.contentDocument || iframe.contentWindow.document;
        iframedoc.body.innerHTML = this.response.Data;
        this.response.Data = '';

        iframe.style.display = 'initial';
      } else {
        iframe.style.display = 'none';
      }

    });
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
  max-height: 500px;
  height: 500px;
  overflow: auto;
}

#results-iframe {
  display: none;
  height: 100%;
  width: 100%;
}

</style>
