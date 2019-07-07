import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { WebbetTicketMatch } from './../../shared/webbet-ticket-match.model';
import { WebbetAppService } from './../../shared/webbet-app.service';
import { Component, OnInit } from '@angular/core';
import { WebbetTicket } from 'src/app/shared/webbet-ticket.model';
import { WebbetTicketService } from 'src/app/shared/webbet-ticket.service';
import { WebbetTicketListComponent } from './webbet-ticket-list.component';

@Component({
  selector: 'app-webbet-ticket',
  templateUrl: './webbet-ticket.component.html',
  styles: []
})
export class WebbetTicketComponent implements OnInit {


  constructor(private service:WebbetAppService,
     private ticketService: WebbetTicketService,
     private dialog: MatDialog) { }

  ngOnInit() {

    this.resetForm();
    this.ticketService.validateTicketForm();
  }
  

  updateTicket(){

    this.ticketService.updateTotalCoefficient();
    this.ticketService.updateStakeWithMtCosts();
    this.ticketService.updatePossibleReturn();
  }

   deletePair(match : WebbetTicketMatch){

    let pairIndex = this.ticketService.ticketFormData.ticketMatches.findIndex(el=> el.id == match.id);
    this.ticketService.ticketFormData.ticketMatches.splice(pairIndex,1); 

    this.updateTicket();
    this.ticketService.validateTicketForm();
  }

  saveTicket(ticket: WebbetTicket){
    
    this.service.postWebbetTicket(ticket).subscribe( res => {
      this.resetForm();
      this.ticketService.updateWallateBalance();
    })
  }

  openTicketList(){
        
      const dialogTicketConfig = new MatDialogConfig();
      dialogTicketConfig.autoFocus = true;
      dialogTicketConfig.disableClose = true;
      dialogTicketConfig.height = '100mv';
      dialogTicketConfig.width = "90%";
      const openTicketDialog = this.dialog.open(WebbetTicketListComponent,dialogTicketConfig);
  }

  resetForm(){
    this.ticketService.ticketFormData = {
      stake: 2,
      ticketCode: '',
      stakeWithManipulationCosts: 0,
      possibleReturn:0,
      totalMatchesCoefficient:0,
      ticketMatches: []
    }
  }
}


