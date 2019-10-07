import { WebbetAppService } from './../shared/webbet-app.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WebbetUserDetails } from '../shared/webbet-user-details.model';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { WebbetTicketService } from '../shared/webbet-ticket.service';
import { WebbetWalletDepositComponent } from './webbet-wallet/webbet-wallet-deposit.component';
import { WebbetUserService } from '../shared/webbet-user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'webbet-app',
  templateUrl: './webbet-app.component.html',
  styles: []
})
export class WebbetAppComponent implements OnInit {
  
  constructor( private router: Router, 
               private dialog: MatDialog,
               private service: WebbetAppService, 
               private userService : WebbetUserService,
               private toastr : ToastrService) { }

  ngOnInit() {
    this.service.getUserDetalis().subscribe(
      res => {
        this.userService.userDetails = res as WebbetUserDetails;
      },
      err => {
        console.log(err);
      }
    )
  }
  
  makeDeposit(){
    const dialogConfig =  new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width = "40%";
    const openDialog = this.dialog.open(WebbetWalletDepositComponent, dialogConfig);
   
    openDialog.afterClosed().subscribe( () => 
            this.service.getUserDetalis().subscribe(
              res => {
                this.userService.userDetails = res as WebbetUserDetails;
             
              }
            )
      );
  }

  onLogout(){
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }
}
