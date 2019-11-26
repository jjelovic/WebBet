import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { WebbetTicketMatch } from './../../shared/webbet-ticket-match.model';
import { WebbetAppService } from './../../shared/webbet-app.service';
import { Component, OnInit } from '@angular/core';
import { WebbetTicket } from 'src/app/shared/webbet-ticket.model';
import { WebbetTicketService } from 'src/app/shared/webbet-ticket.service';
import { WebbetTicketListComponent } from './webbet-ticket-list.component';
import { ToastrService } from 'ngx-toastr';
import { WebbetUserService } from 'src/app/shared/webbet-user.service';
import { WebbetUserDetails } from 'src/app/shared/webbet-user-details.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-webbet-ticket',
  templateUrl: './webbet-ticket.component.html',
  styles: []
})
export class WebbetTicketComponent implements OnInit {


  constructor(private service:WebbetAppService,
     private ticketService: WebbetTicketService,
     private userService : WebbetUserService,
     private dialog: MatDialog,
     private toastr: ToastrService,
     private router: Router) { }

     user : WebbetUserDetails;

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

    let offerMatchIndex = this.ticketService.matchArray.findIndex(el => el.id == match.matchId);
    this.ticketService.matchArray[offerMatchIndex].selectedType = null;
    this.ticketService.matchArray[offerMatchIndex].selectedTopOfferType = null;
    this.ticketService.matchArray.splice(offerMatchIndex, 1);

    let pairIndex = this.ticketService.ticketFormData.ticketMatches.findIndex(el=> el.matchId == match.matchId);
    this.ticketService.ticketFormData.ticketMatches.splice(pairIndex, 1); 

    this.updateTicket();
    this.ticketService.validateTicketForm();
  }

  saveTicket(ticket: WebbetTicket){

    this.service.postWebbetTicket(ticket, this.userService.userDetails.userId).subscribe( res => {
      this.resetForm();

      this.service.getUserDetalis().subscribe( res => {
        this.userService.userDetails = res as WebbetUserDetails;
      })

      this.toastr.success('Listić uspješno spremljen')
     
    })
  }

  openTicketList(){
        
      const dialogTicketConfig = new MatDialogConfig();
      dialogTicketConfig.autoFocus = false;
      dialogTicketConfig.disableClose = true;
      dialogTicketConfig.height = '100mv';
     
      dialogTicketConfig.width = "95%";
    
     this.dialog.open(WebbetTicketListComponent,dialogTicketConfig);
  }

  resetForm(){
    this.ticketService.ticketFormData = {
      ticketId: 0,
      stake: 2,
      ticketCode: '',
      stakeWithManipulationCosts: 0,
      possibleReturn:0,
      totalMatchesCoefficient:0,
      ticketMatches: []
    }
  }
}


