import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { TableComponent } from './table/table.component';
import { FormPopupComponent } from './form-popup/form-popup.component';
import { ConfirmDialogeComponent } from './confirm-dialoge/confirm-dialoge.component';

@NgModule({
  declarations: [
    AppComponent,
    TableComponent,
    FormPopupComponent,
    ConfirmDialogeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
