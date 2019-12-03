import { WebbetUserService } from './../../shared/webbet-user.service';
import { WebbetTicketMatch } from './../../shared/webbet-ticket-match.model';
import { WebbetTicket } from './../../shared/webbet-ticket.model';
import { Component, OnInit } from '@angular/core';
import { WebbetAppService } from 'src/app/shared/webbet-app.service';
import { MatDialogRef } from '@angular/material';
import { WebbetTicketService } from 'src/app/shared/webbet-ticket.service';

@Component({
  selector: 'app-webbet-ticket-list',
  templateUrl: './webbet-ticket-list.component.html',
  styleUrls: []
})
export class WebbetTicketListComponent implements OnInit {

  ticketList: WebbetTicket[];


  constructor(
    public dialogRef: MatDialogRef<WebbetTicketListComponent>, 
    private service: WebbetAppService,
    private ticketService: WebbetTicketService,
    private userService: WebbetUserService
  ) { }

  ngOnInit() {
    this.service.getAllTickets(this.userService.userDetails.userId).subscribe(res => {
        this.ticketList = res as WebbetTicket[];
    })
  }

  previewMatches(ticketMatches: WebbetTicketMatch[], ticketCode: string){

    this.ticketService.ticketMatchesForPreview = ticketMatches;
    this.ticketService.acitveTicketCode = ticketCode;

  }

  deleteTicket(ticketId: number){
    this.service.deleteWebbetTicket(ticketId).subscribe( res => {
            this.service.getAllTickets(this.userService.userDetails.userId).subscribe(res => {
              this.ticketList = res as WebbetTicket[];
              if(this.ticketList.length !== 0){
                 this.ticketService.ticketMatchesForPreview = this.ticketList[0].ticketMatches;
                 this.ticketService.acitveTicketCode = this.ticketList[0].ticketCode;
              }else 
                 this.ticketService.ticketMatchesForPreview = [];
              
          })
    },
    err => {
      console.log(err);
    }
    )
  }
}