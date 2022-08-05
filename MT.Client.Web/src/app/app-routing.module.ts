import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: "transactions",
    loadChildren: () => import("src/app/modules/transaction/transaction.module").then(m => m.TransactionModule)
  },
  {
    path: "categories",
    loadChildren: () => import("src/app/modules/category/category.module").then(m => m.CategoryModule)
  },
  {
    path: "",
    redirectTo: "transactions",
    pathMatch: "full"
  },
  {
    path: "**",
    redirectTo: "transactions",
    pathMatch: "full"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
