import { Component, OnInit, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { WebbetWallet } from 'src/app/shared/webbet-wallet.model';
import { NgForm } from '@angular/forms';
import { WebbetAppService } from 'src/app/shared/webbet-app.service';
 import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { WebbetUserService } from 'src/app/shared/webbet-user.service';



@Component({
  selector: 'app-webbet-wallet-deposit',
  templateUrl: './webbet-wallet-deposit.component.html',
  styles: []
})
export class WebbetWalletDepositComponent implements OnInit {
  
  formData : WebbetWallet;
  isValid :boolean = true;

  constructor( 
    public dialogRef: MatDialogRef<WebbetWalletDepositComponent>, 
    private service: WebbetAppService,
    private toastr: ToastrService,
    private userService: WebbetUserService
    ) { }

  ngOnInit() {
    this.formData = {
       amount : 10
    }
  }

  onSubmit(form:NgForm){

      let user = this.userService.userDetails;

      this.service.postWalletDeposit(form.value, user.userId).subscribe( res=> 
        {
          this.dialogRef.close();
          
          this.toastr.success("Uspješno ste doplatili novčanik");
        },
          err => {
            console.log(err)
          });
    }

    validateForm(formData : WebbetWallet){
    };
}
