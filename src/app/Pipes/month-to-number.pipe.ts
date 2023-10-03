import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'monthToNumber'
})
export class MonthToNumberPipe implements PipeTransform {
  months: string[]=[
    'january',
    'febraury',
    'march',
    'april',
    'may',
    'june',
    'july',
    'august',
    'september',
    'october',
    'november',
    'december'
];

  transform(value: string): string {
    let monthNumber: string='0';    
    this.months.forEach((val,ind) =>{
      if(val.toLocaleLowerCase()===value.toLowerCase()){
        monthNumber=(ind+1).toString();        
      }

    });

    return monthNumber;
  }

}
