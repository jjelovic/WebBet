import { WebbetWallet } from 'src/app/shared/webbet-wallet.model';
import { WebbetWalletDepositComponent } from './webbet-wallet-deposit.component';
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { WebbetAppService } from 'src/app/shared/webbet-app.service';

@Component({
  selector: 'app-webbet-wallet',
  templateUrl: './webbet-wallet.component.html',
  styles: []
})
export class WebbetWalletComponent implements OnInit {

  wallet: WebbetWallet;

  constructor(private dialog:MatDialog ,private service: WebbetAppService) { }

  ngOnInit() {
    this.service.getWalletBalance().subscribe( res => {
      this.wallet = res as WebbetWallet;
    });
  }

  makeDeposit(){
    const dialogConfig =  new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width = "40%";
    this.dialog.open(WebbetWalletDepositComponent,dialogConfig);
  }
}
