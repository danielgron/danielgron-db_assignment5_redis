import { Component } from '@angular/core';
import { OccupationResult } from './_dto/occupation-result'
import { OccupationService } from './_services/occupation.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'RedisSPA';


  occupationResult: OccupationResult | undefined;

  constructor(private occupationService: OccupationService) { }

  getData(){
    this.occupationResult = undefined;
    var occupationObs = this.occupationService.getOccupations(true);
    occupationObs.subscribe(result => {
      this.occupationResult = result;
      console.log(result);
    })
  }

  ngOnInit(): void {
    
  }
}
