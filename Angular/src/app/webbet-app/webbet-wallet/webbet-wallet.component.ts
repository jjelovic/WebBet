import { WebbetTicketService } from './../../shared/webbet-ticket.service';
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


  constructor(private dialog:MatDialog ,private service: WebbetAppService, private ticketService : WebbetTicketService) { }

  ngOnInit() {
    this.service.getWalletBalance().subscribe( res => {
      this.ticketService.wallet = res as WebbetWallet;
    });
  }

  makeDeposit(){
    const dialogConfig =  new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width = "40%";
    const openDialog = this.dialog.open(WebbetWalletDepositComponent,dialogConfig);
   
    openDialog.afterClosed().subscribe(() => this.ticketService.updateWallateBalance());
  }
}
