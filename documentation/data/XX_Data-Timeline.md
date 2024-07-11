# Data Timeline

The data processing pipeline takes the raw data from various source that cover differing time periods


```mermaid
gantt
    title Data timeline
    dateFormat DD-MM-YYYY
        section AAR
            Return period         :a1, 01-09-2023, 31-08-2024
            Online form will go live    :a2, 2024-11-01, 28-01-2025
        section CFR
            Return period         :a1, 01-04-2023, 31-03-2024
        section CDC
            TODO         :a1, 01-04-2023, 1d
        section SEN
            TODO         :a1, 01-04-2023, 1d
        section GIAS
            TODO         :a1, 01-04-2023, 1d        
        section Key stage 2/4
            TODO         :a1, 01-04-2023, 1d    
        section Pupil Census
            TODO         :a1, 01-04-2023, 1d         
        section Workforce Census
            TODO         :a1, 01-04-2023, 1d   
        section BFR
            TODO         :a1, 01-04-2023, 1d    
```

## Dataset period

| Dataset              | Period                      | 
|:---------------------|:----------------------------|
| **GIAS**             |                             | 
| **CDC**              |                             |  
| **SEN**              |                             | 
| **AAR**              | 1st September - 31th August | 
| **CFR**              | 1st April - 31th March      |
| **Key stage 2/4**    |                             | 
| **Pupil Census**     |                             |  
| **Workforce Census** |                             | 
| **BFR**              |                             |  


## Data processing timeline
```mermaid
timeline
    title Data processing
        section Academic year - 2021/22 <br>Financial year - 2020/21
            Data drop 1 <br> Sept <br> (CFR) : AAR (2020) : CFR (2021) : BFR (???)
            Data drop 2 <br> ??? <br> (BFR) :  AAR (2020) : CFR (2021) : BFR (???)
            Data drop 3 <br> Feb <br> (AAR) :  AAR (2021) : CFR (2021) : BFR (???)
        section Academic year - 2022/23 <br>Financial year - 2021/22
            Data drop 1 <br> Sept <br> (CFR) : AAR (2021) : CFR (2022) : BFR (???)
            Data drop 2 <br> ??? <br> (BFR) :  AAR (2021) : CFR (2022) : BFR (???)
            Data drop 3 <br> Feb <br> (AAR) :  AAR (2022) : CFR (2022) : BFR (???)    
        section Academic year - 2023/24 <br>Financial year - 2022/23
            Data drop 1 <br> Sept <br> (CFR) : AAR (2022) : CFR (2023) : BFR (???)
            Data drop 2 <br> ??? <br> (BFR) :  AAR (2022) : CFR (2023) : BFR (???)
            Data drop 3 <br> Feb <br> (AAR) :  AAR (2023) : CFR (2023) : BFR (???)       
```