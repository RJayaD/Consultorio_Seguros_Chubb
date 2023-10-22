export class TxtService {
  public saveDataInTXT(data: Array<any>): string {
    if (data.length == 0) {
      return '';
    }

    let propertyNames = Object.keys(data[0]);
    let rowWithPropertyNames = propertyNames.join(',') + '\n';

    let txtContent = rowWithPropertyNames;

    let rows: string[] = [];

    data.forEach((item) => {
      let values: string[] = [];

      propertyNames.forEach((key) => {
        let val: any = item[key];

        if (val !== undefined && val !== null) {
          val = new String(val);
        } else {
          val = '';
        }
        values.push(val);
      });
      rows.push(values.join(','));
    });
    txtContent += rows.join('\n');

    return txtContent;
  }

  public importDataFromTxt(txtText: string): Array<any> {
    const propertyNames = txtText.slice(0, txtText.indexOf('\n')).split(',');
    const dataRows = txtText.slice(txtText.indexOf('\n') + 1).split('\n');
    let dataArray: any[] = [];
    dataRows.forEach((row) => {
      let values = row.split(',');

      let obj: any = new Object();

      for (let index = 0; index < propertyNames.length; index++) {
        const propertyName: string = propertyNames[index];

        // Verifica si el valor es undefined antes de llamar a trim()
        let val: any = values[index] !== undefined ? values[index].trim() : null;

        obj[propertyName] = val;
      }

      dataArray.push(obj);
    });

    return dataArray;
}

}
