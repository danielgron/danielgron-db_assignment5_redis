import { Occupation } from "./occupation";

export class OccupationResult
{
    occupations: Occupation[];
    timeElapsed: number;


  constructor(occupations: Occupation[], timeElapsed: number) {
    
    this.occupations = occupations;
    this.timeElapsed = timeElapsed;
  }

}
