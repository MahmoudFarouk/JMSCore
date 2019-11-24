import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
@Component({
  selector: 'app-journey-driver',
  templateUrl: './journey-driver.component.html',
  styleUrls: ['./journey-driver.component.css']
})
export class JourneyDriverComponent implements OnInit {
  public JourneyId: number = 0;
  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      const journeyId = params['journeyId'];
      this.JourneyId = journeyId != undefined && journeyId != null && journeyId != '' ? journeyId : 0;

    });
    
  }

}
