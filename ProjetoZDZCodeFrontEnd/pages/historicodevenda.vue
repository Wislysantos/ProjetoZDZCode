<template>
  <v-container>
    <v-card>
      <v-row class="align-center">
        <v-col>
          <v-card-title  class="mr-4">
            Historico de Venda
          </v-card-title>
        </v-col>        
      </v-row>

      <v-dialog v-model="dialog" max-width="500px">
        <v-card>
          <v-card-title>
            <span class="headline">Produtos vendido</span>
          </v-card-title>
          <v-card-text>
            
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="dialog = false">
              voltar
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>

      <v-data-table
        :headers="headers"
        :items="desserts"
        :items-per-page="5"
        class="elevation-1"
        dark
      >
        <template v-slot:top>
          <v-toolbar flat>
            <v-toolbar-title>Vendas</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-text-field
              v-model="search"
              append-icon="mdi-magnify"
              label="Pesquisar"
              single-line
              hide-details
            ></v-text-field>
          </v-toolbar>
        </template>
        <template v-slot:item.actions="{ item }">
          <v-icon small class="align-center" @click="abrirProdutosDavenda(item)">
            mdi-eye
          </v-icon>
        </template>
      </v-data-table>
    </v-card>
  </v-container>
</template>

<script>
export default {
  name: 'IndexPage',
  data () {
    const listProdutos = [
      { name: 'Ice cream sandwich', calories: 237, price: 5, available: 10 },
      { name: 'Eclair', calories: 262, price: 7, available: 5 },
      { name: 'Cupcake', calories: 305, price: 4, available: 8 },
      { name: 'Gingerbread', calories: 356, price: 6, available: 12 },
    ]
    const vendas = [
      { codigDaVenda: '6152', valorTotal: 213.00, dataDaVenda: "05/07/2024", produtos: listProdutos},
      { codigDaVenda: '2313', valorTotal: 237.92, dataDaVenda: "05/07/2024", produtos: listProdutos},
      { codigDaVenda: '7634', valorTotal: 123.51, dataDaVenda: "05/07/2024", produtos: listProdutos},
      { codigDaVenda: '3433', valorTotal: 877.99, dataDaVenda: "05/07/2024", produtos: listProdutos},
      { codigDaVenda: '3234', valorTotal: 455.01, dataDaVenda: "05/07/2024", produtos: listProdutos},
      { codigDaVenda: '4322', valorTotal: 842.05, dataDaVenda: "05/07/2024", produtos: listProdutos},
    
    ];

    

    return {
      dialog: false,
      isEditing: false,
      vendas:{
        codigDaVenda: '',
        valorTotal: 0,
        dataDaVenda: '',
        produtos: []
      },
      listProdutos: {
        codigDaVenda: '',
        calorias: '',
        preco: 0,
        quantidade: 0
      },
      search: '',
      headers: [
        { text: 'Codigo da Venda', align: 'start', sortable: false, value: 'codigDaVenda' },
        { text: 'Valor Total', value: 'valorTotal' },
        { text: 'Data da venda', value: 'dataDaVenda' },
        { text: 'Actions', value: 'actions', sortable: false },
      ],
      desserts: vendas,
    }
  },
  methods: {
    abrirProdutosDavenda(item) {
      this.isEditing = true;
      this.venda = { ...item };
      this.dialog = true;
    },
   
   
  }
}
</script>

<style scoped>
/* Adicione estilos adicionais aqui, se necessário */
</style>
