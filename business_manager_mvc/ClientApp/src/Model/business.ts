export class BusinessDataModel {
  id: number;
  identification: IdentificationData = new IdentificationData();
  businessInfo: BusinessInfo = new BusinessInfo();
  workHours: WorkHoursData[] = [];
  disabled: boolean;
}

export class BusinessInfo {
  id: number;
  address: AddressData = new AddressData();
  phone: string;
  emailBusiness: string;
  urlSite: string;
  urlInstagram: string;
  urlFaceBook: string;
  urlLinkedIn: string;
  photoPath1: string;
  photoPath2: string;
  photoPath3: string;
  photoPath4: string;
  photoPath5: string;
}

export class IdentificationData {
  id: number;
  name: string;
  type: string;
  tva: string;
  emailPro: string;
  description: string;
  logoPath: string;
}
export class AddressData {
  id: number;
  postalCode: string;
  country: string;
  street: string;
  boxNumber: string;
}
export class WorkHoursData {
  id: number;
  day: string;
  hourFrom: number;
  hourTo: number;
  minuteTo: number;
  minuteFrom: number;
  closed: boolean;

  constructor(day: string, hourFrom: number, hourTo: number, minuteTo: number, minuteFrom: number, closed: boolean) {
    this.day = day;
    this.hourFrom = hourFrom;
    this.hourTo = hourTo;
    this.minuteTo = minuteTo;
    this.minuteFrom = minuteFrom;
    this.closed = closed;
  }
}
