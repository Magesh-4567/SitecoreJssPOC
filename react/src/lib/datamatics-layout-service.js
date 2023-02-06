import { RestLayoutService } from '@sitecore-jss/sitecore-jss-react';
import config from '../temp/config';

export class DatamaticsLayoutService extends RestLayoutService {
  constructor(config) {
    super(config);
  }

  getFetchParams = (language) => {
    const queryParams = new URLSearchParams(window.location.search);
    return {
      sc_apikey: config.sitecoreApiKey,
      sc_site: config.jssAppName,
      sc_lang: language || '',
      tracking: config.tracking ?? true,
      orderId: queryParams.get('orderId'),
    };
  };
}
