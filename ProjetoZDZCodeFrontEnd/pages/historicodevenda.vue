<template>
  <v-container>
    <v-card>
      <v-row class="align-center">
        <v-col>
          <v-card-title class="mr-4">
            Historico de Venda
          </v-card-title>
        </v-col>
      </v-row>

      <v-dialog v-model="dialog" max-width="600px">
        <v-card>
          <v-card-title>
            <span class="headline">Produtos vendidos</span>
          </v-card-title>
          <v-card-text>
            <v-data-table
              :headers="productHeaders"
              :items="venda.produtos"
              class="elevation-1"
              dark
            >
              <template v-slot:item.total="{ item }">
                <span>{{ (item.preco * item.quantidade).toFixed(2) }}</span>
              </template>
            </v-data-table>
            <v-divider class="my-4"></v-divider>
            <v-row>
              <v-col class="text-right">
                <v-subheader>Total dos Produtos: R$ {{ calcularTotalProdutos(venda.produtos) }}</v-subheader>
              </v-col>
            </v-row>
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
      { nome: 'Ice cream sandwich', caloreias: 237, preco: 5.92, quantidade: 10 },
      { nome: 'Eclair', caloreias: 262, preco: 7, quantidade: 5 },
      { nome: 'Cupcake', caloreias: 305, preco: 4, quantidade: 8 },
      { nome: 'Gingerbread', caloreias: 356, preco: 6, quantidade: 12 },
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
      venda: {
        codigDaVenda: '',
        valorTotal: 0,
        dataDaVenda: '',
        produtos: []
      },
      search: '',
      headers: [
        { text: 'Codigo da Venda', align: 'start', sortable: false, value: 'codigDaVenda' },
        { text: 'Valor Total', value: 'valorTotal' },
        { text: 'Data da venda', value: 'dataDaVenda' },
        { text: 'Actions', value: 'actions', sortable: false },
      ],
      productHeaders: [
        { text: 'Nome', value: 'nome' },
        { text: 'Calorias', value: 'caloreias' },
        { text: 'Preço', value: 'preco' },
        { text: 'Quantidade', value: 'quantidade' },
        { text: 'Total', value: 'total' },
      ],
      desserts: vendas,
    }
  },
  methods: {
    abrirProdutosDavenda(item) {
      this.venda = { ...item };
      this.dialog = true;
    },
    calcularTotalProdutos(produtos) {
      return produtos.reduce((total, produto) => total + (produto.preco * produto.quantidade), 0).toFixed(2);
    }
  }
}
</script>

<style scoped>
/* Adicione estilos adicionais aqui, se necessário */
</style>
