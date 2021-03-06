/*
 * Licensed to Jasig under one or more contributor license
 * agreements. See the NOTICE file distributed with this work
 * for additional information regarding copyright ownership.
 * Jasig licenses this file to you under the Apache License,
 * Version 2.0 (the "License"); you may not use this file
 * except in compliance with the License.  You may obtain a
 * copy of the License at the following location:
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
//package org.jasig.cas.web;

//import org.jasig.cas.ticket.ServiceTicket;
//import org.jasig.cas.ticket.Ticket;
//import org.jasig.cas.ticket.registry.TicketRegistry;
//import org.perf4j.log4j.GraphingStatisticsAppender;
//import org.springframework.web.servlet.ModelAndView;
//import org.springframework.web.servlet.mvc.AbstractController;

//import javax.servlet.http.HttpRequest;
//import javax.servlet.http.HttpResponse;
//import java.util.*;

/**
 * @author Scott Battaglia
 * @version $Revision$ $Date$
 * @since 3.3.5
 */
public  class StatisticsController : AbstractController {

    private static  int NUMBER_OF_MILLISECONDS_IN_A_DAY = 86400000;

    private static  int NUMBER_OF_MILLISECONDS_IN_AN_HOUR = 3600000;

    private static  int NUMBER_OF_MILLISECONDS_IN_A_MINUTE = 60000;

    private static  int NUMBER_OF_MILLISECONDS_IN_A_SECOND = 1000;

    private  TicketRegistry ticketRegistry;

    private  Date upTimeStartDate = new Date();

    private string casTicketSuffix;

    public StatisticsController( TicketRegistry ticketRegistry) {
        this.ticketRegistry = ticketRegistry;
    }

    public void setCasTicketSuffix( string casTicketSuffix) {
        this.casTicketSuffix = casTicketSuffix;
    }

    @Override
    protected ModelAndView handleRequestInternal( HttpRequest HttpRequest,  HttpResponse HttpResponse)  {
         ModelAndView modelAndView = new ModelAndView("viewStatisticsView");
        modelAndView.addObject("startTime", this.upTimeStartDate);
         double difference = System.currentTimeMillis() - this.upTimeStartDate.getTime();

        modelAndView.addObject("upTime", calculateUptime(difference, new LinkedList<Integer>(Arrays.asList(NUMBER_OF_MILLISECONDS_IN_A_DAY, NUMBER_OF_MILLISECONDS_IN_AN_HOUR, NUMBER_OF_MILLISECONDS_IN_A_MINUTE, NUMBER_OF_MILLISECONDS_IN_A_SECOND, 1)), new LinkedList<string>(Arrays.asList("day","hour","minute","second","millisecond"))));
        modelAndView.addObject("totalMemory", Runtime.getRuntime().totalMemory() / 1024 / 1024);
        modelAndView.addObject("maxMemory", Runtime.getRuntime().maxMemory() / 1024 / 1024);
        modelAndView.addObject("freeMemory", Runtime.getRuntime().freeMemory() / 1024 / 1024);
        modelAndView.addObject("availableProcessors", Runtime.getRuntime().availableProcessors());
        modelAndView.addObject("serverHostName", HttpRequest.getServerName());
        modelAndView.addObject("serverIpAddress", HttpRequest.getLocalAddr());
        modelAndView.addObject("casTicketSuffix", this.casTicketSuffix);

        int unexpiredTgts = 0;
        int unexpiredSts = 0;
        int expiredTgts = 0;
        int expiredSts = 0;

        try {
             Collection<Ticket> tickets = this.ticketRegistry.getTickets();

            for ( Ticket ticket : tickets) {
                if (ticket is ServiceTicket) {
                    if (ticket.isExpired()) {
                        expiredSts++;
                    } else {
                        unexpiredSts++;
                    }
                } else {
                    if (ticket.isExpired()) {
                        expiredTgts++;
                    } else {
                        unexpiredTgts++;
                    }
                }
            }
        } catch ( UnsupportedOperationException e) {
            // this means the ticket registry doesn't support this information.
        }

         Collection<GraphingStatisticsAppender> appenders = GraphingStatisticsAppender.getAllGraphingStatisticsAppenders();

        modelAndView.addObject("unexpiredTgts", unexpiredTgts);
        modelAndView.addObject("unexpiredSts", unexpiredSts);
        modelAndView.addObject("expiredTgts", expiredTgts);
        modelAndView.addObject("expiredSts", expiredSts);
        modelAndView.addObject("pageTitle", modelAndView.getViewName());
        modelAndView.addObject("graphingStatisticAppenders", appenders);

        return modelAndView;
    }

    protected string calculateUptime( double difference,  Queue<Integer> calculations,  Queue<string> labels) {
        if (calculations.isEmpty()) {
            return "";
        }

         int value = calculations.remove();
         double time = Math.floor(difference / value);
         double newDifference = difference - (time * value);
         string currentLabel = labels.remove();
         string label = time == 0 || time > 1 ? currentLabel + "s" : currentLabel;

        return Integer.toString(new Double(time).intValue()) + " "+ label + " " + calculateUptime(newDifference, calculations, labels);
        
    }
}
