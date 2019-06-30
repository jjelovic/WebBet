import { Component, OnInit, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { WebbetWallet } from 'src/app/shared/webbet-wallet.model';
import { NgForm } from '@angular/forms';
import { WebbetAppService } from 'src/app/shared/webbet-app.service';
 import { Router } from '@angular/router';



@Component({
  selector: 'app-webbet-wallet-deposit',
  templateUrl: './webbet-wallet-deposit.component.html',
  styles: []
})
export class WebbetWalletDepositComponent implements OnInit {


  // log(x) {console.log(x);}    html input (change)="log(amount)"
  
  formData : WebbetWallet;
  isValid :boolean = true;

  constructor( 
    public dialogRef: MatDialogRef<WebbetWalletDepositComponent>, 
    private service: WebbetAppService,
    // private router: Router
    ) { }

  ngOnInit() {
    this.formData = {
       amount : 10
    }
  }

  onSubmit(form:NgForm){
      // this.validateForm(form.value);

      this.service.postWalletDeposit(form.value).subscribe( res=> 
        {
          this.dialogRef.close();
          // this.router.navigate(['/webbetApp']);
          this.service.getWalletBalance();
        },
          err => {
            console.log(err)
          });
    }
    validateForm(formData : WebbetWallet){
      
    };
}
