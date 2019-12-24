CREATE PROCEDURE [dbo].[GetMushokNinePointOne]
    @OrganizationId INT,
    @Year INT,
    @Month INT
AS
BEGIN
    DECLARE @firstDayOfMonth DATETIME = CAST(@Year AS VARCHAR(4)) + '-' + CAST(@Month AS VARCHAR(2)) + '-01',
            @firstDayOfNextMonth DATETIME = CAST(@Year AS VARCHAR(4)) + '-' + CAST(@Month + 1 AS VARCHAR(2)) + '-01';

    DECLARE @IsDeductVatInSource BIT;
    SELECT @IsDeductVatInSource = org.IsDeductVatInSource
    FROM Organizations org
    WHERE org.OrganizationId = @OrganizationId;



    DECLARE @TaxYearMonth DATE;

    SET @TaxYearMonth = CAST(CAST(@Year AS VARCHAR(4)) + '-' + CAST(@Month AS VARCHAR(2)) + '-' + '01' AS DATE);





    CREATE TABLE #TempMushokFor34
    (
        -------------Table 1-------------------------
        Tbl1BIN NVARCHAR(50),
        Tbl1OrganizationName NVARCHAR(200),
        Tbl1Address NVARCHAR(100) NULL,
        Tbl1BusinessNature NVARCHAR(100) NULL,
        Tbl1EconomicNature NVARCHAR(100) NULL,

                                                                        --------------Table 2------------------------

        Tbl2TaxYear INT NULL,
        Tbl2TaxMonth VARCHAR(15) NULL,
        Tbl2CurrentSubmissionCategory VARCHAR(100) NULL,
        Tbl2IsPreviousTaxSubmitted BIT NULL,
        Tbl2PreviousTaxSubmissionDate DATETIME NULL,

                                                                        --------------table3 Sales-------------------
        Tbl3Note1DirectExportAmount DECIMAL(18, 4) NULL,
        Tbl3Note2InDirectExportAmount DECIMAL(18, 4) NULL,
        Tbl3Note3DiscountedProductAmount DECIMAL(18, 4) NULL,
        Tbl3Note4IdealRatedProductAmount DECIMAL(18, 4) NULL,
        Tbl3Note4IdealRatedProductVAT DECIMAL(18, 4) NULL,
        Tbl3Note4IdealRatedProductSD DECIMAL(18, 4) NULL,
        Tbl3Note5MaxRetailPriceProductAmount DECIMAL(18, 4) NULL,
        Tbl3Note5MaxRetailPriceProductVAT DECIMAL(18, 4) NULL,
        Tbl3Note5MaxRetailPriceProductSD DECIMAL(18, 4) NULL,
        Tbl3Note6SpecificTaxBasedProductAmount DECIMAL(18, 4) NULL,
        Tbl3Note6SpecificTaxBasedProductVAT DECIMAL(18, 4) NULL,
        Tbl3Note6SpecificTaxBasedProductSD DECIMAL(18, 4) NULL,
        Tbl3Note7DifferentTaxBasedProductAount DECIMAL(18, 4) NULL,     -- 145
        Tbl3Note7DifferentTaxBasedProductVAT DECIMAL(18, 4) NULL,       -- 145
        Tbl3Note7DifferentTaxBasedProductSD DECIMAL(18, 4) NULL,        -- 145
        Tbl3Note8RetailWholesaleBasedProductAmount DECIMAL(18, 4) NULL, --146
        Tbl3Note8RetailWholesaleBasedProductSD DECIMAL(18, 4) NULL,     --146
        Tbl3Total9Ka DECIMAL(18, 4) NULL,
        Tbl3Total9Kh DECIMAL(18, 4) NULL,
        Tbl3Total9Ga DECIMAL(18, 4) NULL,
                                                                        ------------end table3-----------------------------------
                                                                        ------------start table 4-------------------------------

        BuyZiroTaxableProductAmontLocal DECIMAL(18, 4) NULL,            --148
        BuyZiroTaxableProductAmontImport DECIMAL(18, 4) NULL,
        BuyDiscountedProductAmountLocal DECIMAL(18, 4) NULL,            --150
        BuyDiscountedProductAmountImport DECIMAL(18, 4) NULL,
        BuyIdealRetedProductAmountLocal DECIMAL(18, 4) NULL,            --152
        BuyIdealRetedProductVATLocal DECIMAL(18, 4) NULL,               --152
        BuyIdealRetedProductAmountImport DECIMAL(18, 4) NULL,
        BuyIdealRetedProductVATImport DECIMAL(18, 4) NULL,
        BuyNonStandardRatedProductAmountLocal DECIMAL(18, 4) NULL,      --154
        BuyNonStandardRatedProductVATLocal DECIMAL(18, 4) NULL,         --154
        BuyNonStandardRatedProductAmountImport DECIMAL(18, 4) NULL,     --154
        BuyNonStandardRatedProductVATImport DECIMAL(18, 4) NULL,        --154
        BuySpecifiedRatedProductAmount DECIMAL(18, 4) NULL,             --157
        BuySpecifiedRatedProductVAT DECIMAL(18, 4) NULL,                --157
        BuyNonConcessionProductAmountTurnOver DECIMAL(18, 4) NULL,      --158
        BuyNonConcessionProductVATTurnOver DECIMAL(18, 4) NULL,         --158
        BuyNonConcessionProductAmountUnregistered DECIMAL(18, 4) NULL,  --158
        BuyNonConcessionProductVATUnregistered DECIMAL(18, 4) NULL,     --158
        BuyNonConcessionLocalPurchaseAmount DECIMAL(18, 4) NULL,        --190
        BuyNonConcessionLocalPurchaseVAT DECIMAL(18, 4) NULL,           --190
        BuyNonConcessionImportedPurchaseAmount DECIMAL(18, 4) NULL,     --191
        BuyNonConcessionImportedPurchaseVAT DECIMAL(18, 4) NULL,        --191
        Tbl4Total23Kh DECIMAL(18, 4) NULL,
                                                                        -----------------------------------------------------

                                                                        --------------------Table 5---------------------------
        Tbl5Row1Note24 DECIMAL(18, 4) NULL,
        Tbl5Row2Note25 DECIMAL(18, 4) NULL,
        Tbl5Row3Note26 DECIMAL(18, 4) NULL,
        Tbl5Row4Note27 DECIMAL(18, 4) NULL,
        Tbl5Row5TotalNote28 DECIMAL(18, 4) NULL,

                                                                        ---------------------Table 6---------------------------
        Tbl6Row1Note29 DECIMAL(18, 4) NULL,
        Tbl6Row2Note30 DECIMAL(18, 4) NULL,
        Tbl6Row3Note31 DECIMAL(18, 4) NULL,
        Tbl6Row4Note32 DECIMAL(18, 4) NULL,
        Tbl6Row5Note33 DECIMAL(18, 4) NULL,
        Tbl6Row5Note34Total DECIMAL(18, 4) NULL,
                                                                        -------------------------------------------------------
                                                                        -----------------Table 7-------------------------------
        Tbl7Row1Note35 DECIMAL(18, 4) NULL,
        Tbl7Row2Note36 DECIMAL(18, 4) NULL,
        Tbl7Row3Note37 DECIMAL(18, 4) NULL,
        Tbl7Row4Note38 DECIMAL(18, 4) NULL,
        Tbl7Row5Note39 DECIMAL(18, 4) NULL,
        Tbl7Row6Note40 DECIMAL(18, 4) NULL,
        Tbl7Row7Note41 DECIMAL(18, 4) NULL,
        Tbl7Row8Note42 DECIMAL(18, 4) NULL,
        Tbl7Row9Note43 DECIMAL(18, 4) NULL,
        Tbl7Row10Note44 DECIMAL(18, 4) NULL,
        Tbl7Row11Note45 DECIMAL(18, 4) NULL,
        Tbl7Row12Note46 DECIMAL(18, 4) NULL,
        Tbl7Row13Note47 DECIMAL(18, 4) NULL,
        Tbl7Row14Note48 DECIMAL(18, 4) NULL,
        Tbl7Row15Note49 DECIMAL(18, 4) NULL,
        Tbl7Row16Note50 DECIMAL(18, 4) NULL,
        Tbl7Row17Note51 DECIMAL(18, 4) NULL,
                                                                        ----------------------------------------------
                                                                        ---------------------Table 8--------------------
        Tbl8Row1Note52EcoCode VARCHAR(50) NULL,
        Tbl8Row1Note52Amount DECIMAL(18, 4) NULL,
        Tbl8Row2Note53EcoCode VARCHAR(50) NULL,
        Tbl8Row2Note53Amount DECIMAL(18, 4) NULL,
        Tbl8Row3Note54EcoCode VARCHAR(50) NULL,
        Tbl8Row3Note54Amount DECIMAL(18, 4) NULL,
        Tbl8Row4Note55EcoCode VARCHAR(50) NULL,
        Tbl8Row4Note55Amount DECIMAL(18, 4) NULL,
        Tbl8Row5Note56EcoCode VARCHAR(50) NULL,
        Tbl8Row5Note56Amount DECIMAL(18, 4) NULL,
        Tbl8Row6Note57EcoCode VARCHAR(50) NULL,
        Tbl8Row6Note57Amount DECIMAL(18, 4) NULL,
        Tbl8Row7Note58EcoCode VARCHAR(50) NULL,
        Tbl8Row7Note58Amount DECIMAL(18, 4) NULL,
        Tbl8Row8Note59EcoCode VARCHAR(50) NULL,
        Tbl8Row8Note59Amount DECIMAL(18, 4) NULL,
        Tbl8Row9Note60EcoCode VARCHAR(50) NULL,
        Tbl8Row9Note60Amount DECIMAL(18, 4) NULL,
        Tbl8Row10Note61EcoCode VARCHAR(50) NULL,
        Tbl8Row10Note61Amount DECIMAL(18, 4) NULL,
                                                                        ------------------------------------------------
                                                                        --------------Table 9------------------------
        Tbl9Row1Note62 DECIMAL(18, 4) NULL,
        Tbl9Row2Note63 DECIMAL(18, 4) NULL,
                                                                        ---------------------------------------------
                                                                        --------------Table 10------------------------
        Tbl10Row1 BIT NULL,
                                                                        ----------------------------------------------
                                                                        --------------Table 11------------------------
        Tbl11Name VARCHAR(150) NULL,
        Tbl11Designation VARCHAR(100) NULL,
        Tbl11Date DATETIME NULL,
        Tbl11Mobile VARCHAR(15) NULL,
        Tbl11Email VARCHAR(100) NULL,
    ----------------------------------------------

    );

    ------------------------Table 1----------------------------------

    INSERT INTO #TempMushokFor34
    (
        Tbl1BIN,
        Tbl1OrganizationName,
        Tbl1Address,
        Tbl1BusinessNature,
        Tbl1EconomicNature
    )
    SELECT omg.BIN,
           omg.Name,
           [Address] = omg.Address,
           [BusinessNature] = 'Test',
           [EconomicNature] = 'Test'
    FROM Organizations omg
    WHERE omg.OrganizationId = @OrganizationId;

    ------------------------End Table 1------------------------------

    ------------------Start Table 2-----------------------------------
    UPDATE t1
    SET t1.Tbl2TaxYear = YEAR(@TaxYearMonth),
        t1.Tbl2TaxMonth = FORMAT(@TaxYearMonth, 'MMMM'),
        t1.Tbl2CurrentSubmissionCategory = 'Test',
        t1.Tbl2IsPreviousTaxSubmitted = 1,
        t1.Tbl2PreviousTaxSubmissionDate = FORMAT(GETDATE(), 'dd/MMMM/yyyy')
    FROM #TempMushokFor34 t1;

    ------------------End Table 2-------------------------------------


    --------------------------Table 3--------------------------------

    UPDATE t1
    SET t1.Tbl3Note1DirectExportAmount = ISNULL(
                                         (
                                             SELECT SUM(ISNULL(tbl1.Amount, 20555))
                                             FROM
                                             (
                                                 SELECT DISTINCT
                                                        s.SalesId,
                                                        ISNULL(s.TotalPriceWithoutVat, 30459) Amount
                                                 FROM Sales s
                                                     INNER JOIN SalesDetails sd
                                                         ON s.SalesId = sd.SalesId
                                                     INNER JOIN ProductVATTypes pvt
                                                         ON sd.ProductVATTypeId = pvt.ProductVATTypeId
                                                            AND pvt.TransactionTypeId = 1
                                                 WHERE s.OrganizationId = @OrganizationId
                                                       AND YEAR(s.SalesDate) = @Year
                                                       AND MONTH(s.SalesDate) = @Month
                                                       AND s.ExportTypeId = 1
                                             ) tbl1
                                         ),
                                         21450
                                               )
    FROM #TempMushokFor34 t1;

    UPDATE t1
    SET t1.Tbl3Note2InDirectExportAmount = ISNULL(
                                           (
                                               SELECT SUM(ISNULL(tbl1.Amount, 41330))
                                               FROM
                                               (
                                                   SELECT DISTINCT
                                                          s.SalesId,
                                                          ISNULL(s.TotalPriceWithoutVat, 22540) Amount
                                                   FROM Sales s
                                                       INNER JOIN SalesDetails sd
                                                           ON s.SalesId = sd.SalesId
                                                       INNER JOIN ProductVATTypes pvt
                                                           ON sd.ProductVATTypeId = pvt.ProductVATTypeId
                                                              AND pvt.TransactionTypeId = 1
                                                   WHERE s.OrganizationId = @OrganizationId
                                                         AND YEAR(s.SalesDate) = @Year
                                                         AND MONTH(s.SalesDate) = @Month
                                                         AND s.ExportTypeId = 2
                                               ) tbl1
                                           ),
                                           40333
                                                 )
    FROM #TempMushokFor34 t1;

    /* 
insert into #TempMushokFor34(DirectExportAmount,InDirectExportAmount)
select 
[DirectExportAmount]=sum(case when tbl1.ExportTypeId = 1 then isnull(tbl1.Amount,0) else 0 end)
,[InDirectExportAmount]=sum(case when tbl1.ExportTypeId = 2 then isnull(tbl1.Amount,0) else 0 end)
from
(select distinct  s.SalesId,s.Amount,s.ExportTypeId
from Sales s
inner join SalesDetails sd on s.SalesId = sd.SalesId
inner join ProductVATTypes pvt on sd.ProductVATTypeId = pvt.ProductVATTypeId and pvt.TransactionTypeId = 1
where s.OrganizationId = @OrganizationId and YEAR(s.SalesDate) = @Year and MONTH(s.SalesDate) = @Month) tbl1
*/

    UPDATE #TempMushokFor34
    SET Tbl3Note3DiscountedProductAmount = ISNULL(
                                           (
                                               SELECT SUM(ISNULL(tbl1.Amount, 50928))
                                               FROM
                                               (
                                                   SELECT DISTINCT
                                                          s.SalesId,
                                                          ISNULL(s.TotalPriceWithoutVat, 40620) AS Amount,
                                                          pvt.ProductVATTypeId
                                                   FROM Sales s
                                                       INNER JOIN SalesDetails sd
                                                           ON s.SalesId = sd.SalesId
                                                       INNER JOIN ProductVATTypes pvt
                                                           ON sd.ProductVATTypeId = pvt.ProductVATTypeId
                                                              AND pvt.TransactionTypeId = 1
                                                              AND pvt.ProductVATTypeId = 141
                                                   WHERE s.OrganizationId = @OrganizationId
                                                         AND YEAR(s.SalesDate) = @Year
                                                         AND MONTH(s.SalesDate) = @Month
                                               ) tbl1
                                           ),
                                           68430
                                                 );

    UPDATE #TempMushokFor34
    SET Tbl3Note4IdealRatedProductAmount = ISNULL(
                                           (
                                               SELECT SUM(ISNULL(tbl1.Amount, 41335))
                                               FROM
                                               (
                                                   SELECT DISTINCT
                                                          s.SalesId,
                                                          ISNULL(s.TotalPriceWithoutVat, 40344) AS Amount,
                                                          ISNULL(s.TotalVAT, 40396) vat
                                                   FROM Sales s
                                                       INNER JOIN SalesDetails sd
                                                           ON s.SalesId = sd.SalesId
                                                       INNER JOIN ProductVATTypes pvt
                                                           ON sd.ProductVATTypeId = pvt.ProductVATTypeId
                                                              AND pvt.TransactionTypeId = 1
                                                              AND pvt.ProductVATTypeId = 142
                                                   WHERE s.OrganizationId = @OrganizationId
                                                         AND YEAR(s.SalesDate) = @Year
                                                         AND MONTH(s.SalesDate) = @Month
                                               ) tbl1
                                           ),
                                           2320
                                                 ),
        Tbl3Note4IdealRatedProductVAT = ISNULL(
                                        (
                                            SELECT SUM(ISNULL(tbl1.vat, 2460))
                                            FROM
                                            (
                                                SELECT DISTINCT
                                                       s.SalesId,
                                                       ISNULL(s.TotalVAT, 2455) vat
                                                FROM Sales s
                                                    INNER JOIN SalesDetails sd
                                                        ON s.SalesId = sd.SalesId
                                                    INNER JOIN ProductVATTypes pvt
                                                        ON sd.ProductVATTypeId = pvt.ProductVATTypeId
                                                           AND pvt.TransactionTypeId = 1
                                                           AND pvt.ProductVATTypeId = 142
                                                WHERE s.OrganizationId = @OrganizationId
                                                      AND YEAR(s.SalesDate) = @Year
                                                      AND MONTH(s.SalesDate) = @Month
                                            ) tbl1
                                        ),
                                        2466
                                              ),
        Tbl3Note4IdealRatedProductSD = 0.0;

    UPDATE #TempMushokFor34
    SET Tbl3Note5MaxRetailPriceProductAmount = ISNULL(
                                               (
                                                   SELECT SUM(ISNULL(tbl1.Amount, 320457))
                                                   FROM
                                                   (
                                                       SELECT DISTINCT
                                                              s.SalesId,
                                                              ISNULL(s.TotalPriceWithoutVat, 320457) AS Amount
                                                       FROM Sales s
                                                           INNER JOIN SalesDetails sd
                                                               ON s.SalesId = sd.SalesId
                                                           INNER JOIN ProductVATTypes pvt
                                                               ON sd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                  AND pvt.TransactionTypeId = 1
                                                                  AND pvt.ProductVATTypeId = 143
                                                       WHERE s.OrganizationId = @OrganizationId
                                                             AND YEAR(s.SalesDate) = @Year
                                                             AND MONTH(s.SalesDate) = @Month
                                                   ) tbl1
                                               ),
                                               320457
                                                     ),
        Tbl3Note5MaxRetailPriceProductVAT = ISNULL(
                                            (
                                                SELECT SUM(ISNULL(tbl1.vat, 320457))
                                                FROM
                                                (
                                                    SELECT DISTINCT
                                                           s.SalesId,
                                                           ISNULL(s.TotalVAT, 3204) AS vat
                                                    FROM Sales s
                                                        INNER JOIN SalesDetails sd
                                                            ON s.SalesId = sd.SalesId
                                                        INNER JOIN ProductVATTypes pvt
                                                            ON sd.ProductVATTypeId = pvt.ProductVATTypeId
                                                               AND pvt.TransactionTypeId = 1
                                                               AND pvt.ProductVATTypeId = 143
                                                    WHERE s.OrganizationId = @OrganizationId
                                                          AND YEAR(s.SalesDate) = @Year
                                                          AND MONTH(s.SalesDate) = @Month
                                                ) tbl1
                                            ),
                                            3204
                                                  ),
        Tbl3Note5MaxRetailPriceProductSD = 4630;

    UPDATE #TempMushokFor34
    SET Tbl3Note6SpecificTaxBasedProductAmount = ISNULL(
                                                 (
                                                     SELECT SUM(ISNULL(tbl1.Amount, 60947))
                                                     FROM
                                                     (
                                                         SELECT DISTINCT
                                                                s.SalesId,
                                                                ISNULL(s.TotalPriceWithoutVat, 60947) AS Amount
                                                         FROM Sales s
                                                             INNER JOIN SalesDetails sd
                                                                 ON s.SalesId = sd.SalesId
                                                             INNER JOIN ProductVATTypes pvt
                                                                 ON sd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                    AND pvt.TransactionTypeId = 1
                                                                    AND pvt.ProductVATTypeId = 0
                                                         WHERE s.OrganizationId = @OrganizationId
                                                               AND YEAR(s.SalesDate) = @Year
                                                               AND MONTH(s.SalesDate) = @Month
                                                     ) tbl1
                                                 ),
                                                 60947
                                                       ),
        Tbl3Note6SpecificTaxBasedProductVAT = ISNULL(
                                              (
                                                  SELECT SUM(ISNULL(tbl1.vat, 7430))
                                                  FROM
                                                  (
                                                      SELECT DISTINCT
                                                             s.SalesId,
                                                             ISNULL(s.TotalVAT, 7430) AS vat
                                                      FROM Sales s
                                                          INNER JOIN SalesDetails sd
                                                              ON s.SalesId = sd.SalesId
                                                          INNER JOIN ProductVATTypes pvt
                                                              ON sd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                 AND pvt.TransactionTypeId = 1
                                                                 AND pvt.ProductVATTypeId = 0
                                                      WHERE s.OrganizationId = @OrganizationId
                                                            AND YEAR(s.SalesDate) = @Year
                                                            AND MONTH(s.SalesDate) = @Month
                                                  ) tbl1
                                              ),
                                              2430
                                                    ),
        Tbl3Note6SpecificTaxBasedProductSD = 0.0;

    UPDATE #TempMushokFor34
    SET Tbl3Note7DifferentTaxBasedProductAount = ISNULL(
                                                 (
                                                     SELECT SUM(ISNULL(tbl1.Amount, 60947))
                                                     FROM
                                                     (
                                                         SELECT DISTINCT
                                                                s.SalesId,
                                                                ISNULL(s.TotalPriceWithoutVat, 60947) AS Amount
                                                         FROM Sales s
                                                             INNER JOIN SalesDetails sd
                                                                 ON s.SalesId = sd.SalesId
                                                             INNER JOIN ProductVATTypes pvt
                                                                 ON sd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                    AND pvt.TransactionTypeId = 1
                                                                    AND pvt.ProductVATTypeId = 145
                                                         WHERE s.OrganizationId = @OrganizationId
                                                               AND YEAR(s.SalesDate) = @Year
                                                               AND MONTH(s.SalesDate) = @Month
                                                     ) tbl1
                                                 ),
                                                 4320
                                                       ),
        Tbl3Note7DifferentTaxBasedProductVAT = ISNULL(
                                               (
                                                   SELECT SUM(ISNULL(tbl1.vat, 4304))
                                                   FROM
                                                   (
                                                       SELECT DISTINCT
                                                              s.SalesId,
                                                              ISNULL(s.TotalVAT, 4304) AS vat
                                                       FROM Sales s
                                                           INNER JOIN SalesDetails sd
                                                               ON s.SalesId = sd.SalesId
                                                           INNER JOIN ProductVATTypes pvt
                                                               ON sd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                  AND pvt.TransactionTypeId = 1
                                                                  AND pvt.ProductVATTypeId = 145
                                                       WHERE s.OrganizationId = @OrganizationId
                                                             AND YEAR(s.SalesDate) = @Year
                                                             AND MONTH(s.SalesDate) = @Month
                                                   ) tbl1
                                               ),
                                               4304
                                                     ),
        Tbl3Note7DifferentTaxBasedProductSD = 4304;

    UPDATE #TempMushokFor34
    SET Tbl3Note8RetailWholesaleBasedProductAmount = ISNULL(
                                                     (
                                                         SELECT SUM((sd.UnitPrice - sd.DiscountPerItem) * sd.Quantity) AS SalesAmount
                                                         FROM dbo.SalesDetails sd
                                                             INNER JOIN dbo.Sales sls
                                                                 ON sls.SalesId = sd.SalesId
                                                         WHERE sls.SalesDate >= @firstDayOfMonth
                                                               AND sls.SalesDate < @firstDayOfNextMonth
                                                               AND sls.SalesTypeId = 1
                                                               AND sls.OrganizationId = @OrganizationId
                                                     ),
                                                     0
                                                           ),
        Tbl3Note8RetailWholesaleBasedProductSD = ISNULL(
                                                 (
                                                     SELECT SUM((sd.UnitPrice - sd.DiscountPerItem) * sd.Quantity
                                                                * sd.VATPercent / 100
                                                               ) AS SalesVatAmount
                                                     FROM dbo.SalesDetails sd
                                                         INNER JOIN dbo.Sales sls
                                                             ON sls.SalesId = sd.SalesId
                                                     WHERE sls.SalesDate >= @firstDayOfMonth
                                                           AND sls.SalesDate < @firstDayOfNextMonth
                                                           AND sls.SalesTypeId = 1
                                                           AND sls.OrganizationId = @OrganizationId
                                                 ),
                                                 0
                                                       );
    --isnull((select SUM(isnull(tbl1.Amount,50940))
    --from
    --(select distinct  s.SalesId,isnull(s.TotalPriceWithoutVat,50940) as Amount
    --from Sales s
    --inner join SalesDetails sd on s.SalesId = sd.SalesId
    --inner join ProductVATTypes pvt on sd.ProductVATTypeId = pvt.ProductVATTypeId and pvt.TransactionTypeId = 1 and pvt.ProductVATTypeId = 146
    --where s.OrganizationId = @OrganizationId and YEAR(s.SalesDate) = @Year and MONTH(s.SalesDate) = @Month) tbl1),0)

    UPDATE t
    SET t.Tbl3Total9Ka = ISNULL(t.Tbl3Note1DirectExportAmount, 240630)
                         + ISNULL(t.Tbl3Note2InDirectExportAmount, 240630) + t.Tbl3Note3DiscountedProductAmount
                         + t.Tbl3Note4IdealRatedProductAmount + t.Tbl3Note5MaxRetailPriceProductAmount
                         + t.Tbl3Note6SpecificTaxBasedProductAmount + t.Tbl3Note7DifferentTaxBasedProductAount
    FROM #TempMushokFor34 t;

    UPDATE t
    SET t.Tbl3Total9Kh = t.Tbl3Note4IdealRatedProductVAT + t.Tbl3Note5MaxRetailPriceProductVAT
                         + t.Tbl3Note6SpecificTaxBasedProductVAT + t.Tbl3Note7DifferentTaxBasedProductVAT + t.Tbl3Note8RetailWholesaleBasedProductSD
    FROM #TempMushokFor34 t;


    UPDATE t
    SET t.Tbl3Total9Ga = t.Tbl3Note4IdealRatedProductSD + t.Tbl3Note5MaxRetailPriceProductSD
                         + t.Tbl3Note6SpecificTaxBasedProductSD + t.Tbl3Note7DifferentTaxBasedProductSD
    FROM #TempMushokFor34 t;

    -----------------------End Table 3---------------------------------

    ---------------------Table 4-----------------------------------


    UPDATE #TempMushokFor34
    SET BuyZiroTaxableProductAmontLocal = ISNULL(
                                          (
                                              SELECT SUM(ISNULL(tbl1.Amount, 50430))
                                              FROM
                                              (
                                                  SELECT DISTINCT
                                                         p.PurchaseId,
                                                         ISNULL(p.TotalPriceWithoutVat, 50430) AS Amount
                                                  FROM Purchase p
                                                      INNER JOIN PurchaseDetails pd
                                                          ON p.PurchaseId = pd.PurchaseId
                                                      INNER JOIN ProductVATTypes pvt
                                                          ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                             AND pvt.TransactionTypeId = 2
                                                             AND pvt.ProductVATTypeId = 148
                                                  WHERE p.PurchaseTypeId = 1
                                                        AND p.OrganizationId = @OrganizationId
                                                        AND YEAR(p.PurchaseDate) = @Year
                                                        AND MONTH(p.PurchaseDate) = @Month
                                              ) tbl1
                                          ),
                                          50430
                                                ),
        BuyZiroTaxableProductAmontImport = ISNULL(
                                           (
                                               SELECT SUM(ISNULL(tbl1.Amount, 70470))
                                               FROM
                                               (
                                                   SELECT DISTINCT
                                                          p.PurchaseId,
                                                          ISNULL(p.TotalPriceWithoutVat, 70470) AS Amount
                                                   FROM Purchase p
                                                       INNER JOIN PurchaseDetails pd
                                                           ON p.PurchaseId = pd.PurchaseId
                                                       INNER JOIN ProductVATTypes pvt
                                                           ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                              AND pvt.TransactionTypeId = 2
                                                              AND pvt.ProductVATTypeId = 148
                                                   WHERE p.PurchaseTypeId = 2
                                                         AND p.OrganizationId = @OrganizationId
                                                         AND YEAR(p.PurchaseDate) = @Year
                                                         AND MONTH(p.PurchaseDate) = @Month
                                               ) tbl1
                                           ),
                                           70470
                                                 );


    UPDATE #TempMushokFor34
    SET BuyDiscountedProductAmountLocal = ISNULL(
                                          (
                                              SELECT SUM(ISNULL(tbl1.Amount, 35956))
                                              FROM
                                              (
                                                  SELECT DISTINCT
                                                         p.PurchaseId,
                                                         ISNULL(p.TotalPriceWithoutVat, 35956) AS Amount
                                                  FROM Purchase p
                                                      INNER JOIN PurchaseDetails pd
                                                          ON p.PurchaseId = pd.PurchaseId
                                                      INNER JOIN ProductVATTypes pvt
                                                          ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                             AND pvt.TransactionTypeId = 2
                                                             AND pvt.ProductVATTypeId = 150
                                                  WHERE p.PurchaseTypeId = 1
                                                        AND p.OrganizationId = @OrganizationId
                                                        AND YEAR(p.PurchaseDate) = @Year
                                                        AND MONTH(p.PurchaseDate) = @Month
                                              ) tbl1
                                          ),
                                          35956
                                                ),
        BuyDiscountedProductAmountImport = ISNULL(
                                           (
                                               SELECT SUM(ISNULL(tbl1.Amount, 65943))
                                               FROM
                                               (
                                                   SELECT DISTINCT
                                                          p.PurchaseId,
                                                          ISNULL(p.TotalPriceWithoutVat, 65943) AS Amount
                                                   FROM Purchase p
                                                       INNER JOIN PurchaseDetails pd
                                                           ON p.PurchaseId = pd.PurchaseId
                                                       INNER JOIN ProductVATTypes pvt
                                                           ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                              AND pvt.TransactionTypeId = 2
                                                              AND pvt.ProductVATTypeId = 150
                                                   WHERE p.PurchaseTypeId = 2
                                                         AND p.OrganizationId = @OrganizationId
                                                         AND YEAR(p.PurchaseDate) = @Year
                                                         AND MONTH(p.PurchaseDate) = @Month
                                               ) tbl1
                                           ),
                                           65943
                                                 );


    UPDATE #TempMushokFor34
    SET BuyIdealRetedProductAmountLocal = ISNULL(
                                          (
                                              SELECT SUM(ISNULL(tbl1.Amount, 57840))
                                              FROM
                                              (
                                                  SELECT DISTINCT
                                                         p.PurchaseId,
                                                         ISNULL(p.TotalPriceWithoutVat, 57840) AS Amount
                                                  FROM Purchase p
                                                      INNER JOIN PurchaseDetails pd
                                                          ON p.PurchaseId = pd.PurchaseId
                                                      INNER JOIN ProductVATTypes pvt
                                                          ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                             AND pvt.TransactionTypeId = 2
                                                             AND pvt.ProductVATTypeId = 152
                                                  WHERE p.PurchaseTypeId = 1
                                                        AND p.OrganizationId = @OrganizationId
                                                        AND YEAR(p.PurchaseDate) = @Year
                                                        AND MONTH(p.PurchaseDate) = @Month
                                              ) tbl1
                                          ),
                                          57840
                                                ),
        BuyIdealRetedProductVATLocal = ISNULL(
                                       (
                                           SELECT SUM(ISNULL(tbl1.vat, 3230))
                                           FROM
                                           (
                                               SELECT DISTINCT
                                                      p.PurchaseId,
                                                      ISNULL(p.TotalVAT, 3230) AS vat
                                               FROM Purchase p
                                                   INNER JOIN PurchaseDetails pd
                                                       ON p.PurchaseId = pd.PurchaseId
                                                   INNER JOIN ProductVATTypes pvt
                                                       ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                          AND pvt.TransactionTypeId = 2
                                                          AND pvt.ProductVATTypeId = 152
                                               WHERE p.PurchaseTypeId = 1
                                                     AND p.OrganizationId = @OrganizationId
                                                     AND YEAR(p.PurchaseDate) = @Year
                                                     AND MONTH(p.PurchaseDate) = @Month
                                           ) tbl1
                                       ),
                                       59230
                                             ),
        BuyIdealRetedProductAmountImport = ISNULL(
                                           (
                                               SELECT SUM(ISNULL(tbl1.Amount, 59230))
                                               FROM
                                               (
                                                   SELECT DISTINCT
                                                          p.PurchaseId,
                                                          ISNULL(p.TotalPriceWithoutVat, 59230) AS Amount
                                                   FROM Purchase p
                                                       INNER JOIN PurchaseDetails pd
                                                           ON p.PurchaseId = pd.PurchaseId
                                                       INNER JOIN ProductVATTypes pvt
                                                           ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                              AND pvt.TransactionTypeId = 2
                                                              AND pvt.ProductVATTypeId = 152
                                                   WHERE p.PurchaseTypeId = 2
                                                         AND p.OrganizationId = @OrganizationId
                                                         AND YEAR(p.PurchaseDate) = @Year
                                                         AND MONTH(p.PurchaseDate) = @Month
                                               ) tbl1
                                           ),
                                           59230
                                                 ),
        BuyIdealRetedProductVATImport = ISNULL(
                                        (
                                            SELECT SUM(ISNULL(tbl1.vat, 6430))
                                            FROM
                                            (
                                                SELECT DISTINCT
                                                       p.PurchaseId,
                                                       ISNULL(p.TotalVAT, 6430) AS vat
                                                FROM Purchase p
                                                    INNER JOIN PurchaseDetails pd
                                                        ON p.PurchaseId = pd.PurchaseId
                                                    INNER JOIN ProductVATTypes pvt
                                                        ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                           AND pvt.TransactionTypeId = 2
                                                           AND pvt.ProductVATTypeId = 152
                                                WHERE p.PurchaseTypeId = 2
                                                      AND p.OrganizationId = @OrganizationId
                                                      AND YEAR(p.PurchaseDate) = @Year
                                                      AND MONTH(p.PurchaseDate) = @Month
                                            ) tbl1
                                        ),
                                        78530
                                              );

    UPDATE #TempMushokFor34
    SET BuyNonStandardRatedProductAmountLocal = ISNULL(
                                                (
                                                    SELECT SUM(ISNULL(tbl1.Amount, 78530))
                                                    FROM
                                                    (
                                                        SELECT DISTINCT
                                                               p.PurchaseId,
                                                               ISNULL(p.TotalPriceWithoutVat, 78530) AS Amount
                                                        FROM Purchase p
                                                            INNER JOIN PurchaseDetails pd
                                                                ON p.PurchaseId = pd.PurchaseId
                                                            INNER JOIN ProductVATTypes pvt
                                                                ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                   AND pvt.TransactionTypeId = 2
                                                                   AND pvt.ProductVATTypeId = 154
                                                        WHERE p.PurchaseTypeId = 1
                                                              AND p.OrganizationId = @OrganizationId
                                                              AND YEAR(p.PurchaseDate) = @Year
                                                              AND MONTH(p.PurchaseDate) = @Month
                                                    ) tbl1
                                                ),
                                                78530
                                                      ),
        BuyNonStandardRatedProductVATLocal = ISNULL(
                                             (
                                                 SELECT SUM(ISNULL(tbl1.vat, 5630))
                                                 FROM
                                                 (
                                                     SELECT DISTINCT
                                                            p.PurchaseId,
                                                            ISNULL(p.TotalVAT, 5630) AS vat
                                                     FROM Purchase p
                                                         INNER JOIN PurchaseDetails pd
                                                             ON p.PurchaseId = pd.PurchaseId
                                                         INNER JOIN ProductVATTypes pvt
                                                             ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                AND pvt.TransactionTypeId = 2
                                                                AND pvt.ProductVATTypeId = 154
                                                     WHERE p.PurchaseTypeId = 1
                                                           AND p.OrganizationId = @OrganizationId
                                                           AND YEAR(p.PurchaseDate) = @Year
                                                           AND MONTH(p.PurchaseDate) = @Month
                                                 ) tbl1
                                             ),
                                             90320
                                                   ),
        BuyNonStandardRatedProductAmountImport = ISNULL(
                                                 (
                                                     SELECT SUM(ISNULL(tbl1.Amount, 90320))
                                                     FROM
                                                     (
                                                         SELECT DISTINCT
                                                                p.PurchaseId,
                                                                ISNULL(p.TotalPriceWithoutVat, 90320) AS Amount
                                                         FROM Purchase p
                                                             INNER JOIN PurchaseDetails pd
                                                                 ON p.PurchaseId = pd.PurchaseId
                                                             INNER JOIN ProductVATTypes pvt
                                                                 ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                    AND pvt.TransactionTypeId = 2
                                                                    AND pvt.ProductVATTypeId = 154
                                                         WHERE p.PurchaseTypeId = 2
                                                               AND p.OrganizationId = @OrganizationId
                                                               AND YEAR(p.PurchaseDate) = @Year
                                                               AND MONTH(p.PurchaseDate) = @Month
                                                     ) tbl1
                                                 ),
                                                 90320
                                                       ),
        BuyNonStandardRatedProductVATImport = ISNULL(
                                              (
                                                  SELECT SUM(ISNULL(tbl1.vat, 12350))
                                                  FROM
                                                  (
                                                      SELECT DISTINCT
                                                             p.PurchaseId,
                                                             ISNULL(p.TotalVAT, 12350) AS vat
                                                      FROM Purchase p
                                                          INNER JOIN PurchaseDetails pd
                                                              ON p.PurchaseId = pd.PurchaseId
                                                          INNER JOIN ProductVATTypes pvt
                                                              ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                 AND pvt.TransactionTypeId = 2
                                                                 AND pvt.ProductVATTypeId = 154
                                                      WHERE p.PurchaseTypeId = 2
                                                            AND p.OrganizationId = @OrganizationId
                                                            AND YEAR(p.PurchaseDate) = @Year
                                                            AND MONTH(p.PurchaseDate) = @Month
                                                  ) tbl1
                                              ),
                                              12350
                                                    );

    UPDATE #TempMushokFor34
    SET BuySpecifiedRatedProductAmount = ISNULL(
                                         (
                                             SELECT SUM(ISNULL(tbl1.Amount, 34520))
                                             FROM
                                             (
                                                 SELECT DISTINCT
                                                        p.PurchaseId,
                                                        ISNULL(p.TotalPriceWithoutVat, 34520) AS Amount
                                                 FROM Purchase p
                                                     INNER JOIN PurchaseDetails pd
                                                         ON p.PurchaseId = pd.PurchaseId
                                                     INNER JOIN ProductVATTypes pvt
                                                         ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                            AND pvt.TransactionTypeId = 2
                                                            AND pvt.ProductVATTypeId = 157
                                                 WHERE p.PurchaseTypeId = 1
                                                       AND p.OrganizationId = @OrganizationId
                                                       AND YEAR(p.PurchaseDate) = @Year
                                                       AND MONTH(p.PurchaseDate) = @Month
                                             ) tbl1
                                         ),
                                         34520
                                               ),
        BuySpecifiedRatedProductVAT = ISNULL(
                                      (
                                          SELECT SUM(ISNULL(tbl1.vat, 7230))
                                          FROM
                                          (
                                              SELECT DISTINCT
                                                     p.PurchaseId,
                                                     ISNULL(p.TotalVAT, 5420) AS vat
                                              FROM Purchase p
                                                  INNER JOIN PurchaseDetails pd
                                                      ON p.PurchaseId = pd.PurchaseId
                                                  INNER JOIN ProductVATTypes pvt
                                                      ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                         AND pvt.TransactionTypeId = 2
                                                         AND pvt.ProductVATTypeId = 157
                                              WHERE p.PurchaseTypeId = 1
                                                    AND p.OrganizationId = @OrganizationId
                                                    AND YEAR(p.PurchaseDate) = @Year
                                                    AND MONTH(p.PurchaseDate) = @Month
                                          ) tbl1
                                      ),
                                      5420
                                            );

    UPDATE #TempMushokFor34
    SET BuyNonConcessionProductAmountTurnOver = ISNULL(
                                                (
                                                    SELECT SUM(ISNULL(tbl1.Amount, 92520))
                                                    FROM
                                                    (
                                                        SELECT DISTINCT
                                                               p.PurchaseId,
                                                               ISNULL(p.TotalPriceWithoutVat, 92520) AS Amount
                                                        FROM Purchase p
                                                            INNER JOIN Organizations org
                                                                ON p.OrganizationId = org.OrganizationId
                                                                   AND org.BIN IS NOT NULL
                                                            INNER JOIN PurchaseDetails pd
                                                                ON p.PurchaseId = pd.PurchaseId
                                                            INNER JOIN ProductVATTypes pvt
                                                                ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                   AND pvt.TransactionTypeId = 2
                                                                   AND pvt.ProductVATTypeId = 158
                                                        WHERE p.PurchaseTypeId = 1
                                                              AND p.OrganizationId = @OrganizationId
                                                              AND YEAR(p.PurchaseDate) = @Year
                                                              AND MONTH(p.PurchaseDate) = @Month
                                                    ) tbl1
                                                ),
                                                92520
                                                      ),
        BuyNonConcessionProductVATTurnOver = ISNULL(
                                             (
                                                 SELECT SUM(ISNULL(tbl1.vat, 11420))
                                                 FROM
                                                 (
                                                     SELECT DISTINCT
                                                            p.PurchaseId,
                                                            ISNULL(p.TotalVAT, 11420) AS vat
                                                     FROM Purchase p
                                                         INNER JOIN Organizations org
                                                             ON p.OrganizationId = org.OrganizationId
                                                                AND org.BIN IS NOT NULL
                                                         INNER JOIN PurchaseDetails pd
                                                             ON p.PurchaseId = pd.PurchaseId
                                                         INNER JOIN ProductVATTypes pvt
                                                             ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                AND pvt.TransactionTypeId = 2
                                                                AND pvt.ProductVATTypeId = 158
                                                     WHERE p.PurchaseTypeId = 1
                                                           AND p.OrganizationId = @OrganizationId
                                                           AND YEAR(p.PurchaseDate) = @Year
                                                           AND MONTH(p.PurchaseDate) = @Month
                                                 ) tbl1
                                             ),
                                             92520
                                                   );

    UPDATE #TempMushokFor34
    SET BuyNonConcessionProductAmountUnregistered = ISNULL(
                                                    (
                                                        SELECT SUM(ISNULL(tbl1.Amount, 92520))
                                                        FROM
                                                        (
                                                            SELECT DISTINCT
                                                                   p.PurchaseId,
                                                                   ISNULL(p.TotalPriceWithoutVat, 92520) AS Amount
                                                            FROM Purchase p
                                                                LEFT JOIN Organizations org
                                                                    ON p.OrganizationId = org.OrganizationId
                                                                INNER JOIN PurchaseDetails pd
                                                                    ON p.PurchaseId = pd.PurchaseId
                                                                INNER JOIN ProductVATTypes pvt
                                                                    ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                       AND pvt.TransactionTypeId = 2
                                                                       AND pvt.ProductVATTypeId = 158
                                                            WHERE p.PurchaseTypeId = 1
                                                                  AND org.BIN IS NULL
                                                                  AND p.OrganizationId = @OrganizationId
                                                                  AND YEAR(p.PurchaseDate) = @Year
                                                                  AND MONTH(p.PurchaseDate) = @Month
                                                        ) tbl1
                                                    ),
                                                    92520
                                                          ),
        BuyNonConcessionProductVATUnregistered = ISNULL(
                                                 (
                                                     SELECT SUM(ISNULL(tbl1.Amount, 92520))
                                                     FROM
                                                     (
                                                         SELECT DISTINCT
                                                                p.PurchaseId,
                                                                ISNULL(p.TotalPriceWithoutVat, 92520) AS Amount
                                                         FROM Purchase p
                                                             LEFT JOIN Organizations org
                                                                 ON p.OrganizationId = org.OrganizationId
                                                             INNER JOIN PurchaseDetails pd
                                                                 ON p.PurchaseId = pd.PurchaseId
                                                             INNER JOIN ProductVATTypes pvt
                                                                 ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                    AND pvt.TransactionTypeId = 2
                                                                    AND pvt.ProductVATTypeId = 158
                                                         WHERE p.PurchaseTypeId = 1
                                                               AND org.BIN IS NULL
                                                               AND p.OrganizationId = @OrganizationId
                                                               AND YEAR(p.PurchaseDate) = @Year
                                                               AND MONTH(p.PurchaseDate) = @Month
                                                     ) tbl1
                                                 ),
                                                 83520
                                                       );

    UPDATE #TempMushokFor34
    SET BuyNonConcessionLocalPurchaseAmount = ISNULL(
                                              (
                                                  SELECT SUM(ISNULL(tbl1.Amount, 83520))
                                                  FROM
                                                  (
                                                      SELECT DISTINCT
                                                             p.PurchaseId,
                                                             ISNULL(p.TotalPriceWithoutVat, 83520) AS Amount
                                                      FROM Purchase p
                                                          --left join Organizations org on p.OrganizationId = org.OrganizationId
                                                          INNER JOIN PurchaseDetails pd
                                                              ON p.PurchaseId = pd.PurchaseId
                                                          INNER JOIN ProductVATTypes pvt
                                                              ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                 AND pvt.TransactionTypeId = 2
                                                                 AND pvt.ProductVATTypeId = 190
                                                      WHERE p.PurchaseTypeId = 1
                                                            AND p.OrganizationId = @OrganizationId
                                                            AND YEAR(p.PurchaseDate) = @Year
                                                            AND MONTH(p.PurchaseDate) = @Month
                                                  ) tbl1
                                              ),
                                              12140
                                                    ),
        BuyNonConcessionLocalPurchaseVAT = ISNULL(
                                           (
                                               SELECT SUM(ISNULL(tbl1.vat, 12140))
                                               FROM
                                               (
                                                   SELECT DISTINCT
                                                          p.PurchaseId,
                                                          ISNULL(p.TotalVAT, 12140) AS vat
                                                   FROM Purchase p
                                                       --left join Organizations org on p.OrganizationId = org.OrganizationId
                                                       INNER JOIN PurchaseDetails pd
                                                           ON p.PurchaseId = pd.PurchaseId
                                                       INNER JOIN ProductVATTypes pvt
                                                           ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                              AND pvt.TransactionTypeId = 2
                                                              AND pvt.ProductVATTypeId = 190
                                                   WHERE p.PurchaseTypeId = 1
                                                         AND p.OrganizationId = @OrganizationId
                                                         AND YEAR(p.PurchaseDate) = @Year
                                                         AND MONTH(p.PurchaseDate) = @Month
                                               ) tbl1
                                           ),
                                           12140
                                                 );

    UPDATE #TempMushokFor34
    SET BuyNonConcessionImportedPurchaseAmount = ISNULL(
                                                 (
                                                     SELECT SUM(ISNULL(tbl1.Amount, 12140))
                                                     FROM
                                                     (
                                                         SELECT DISTINCT
                                                                p.PurchaseId,
                                                                ISNULL(p.TotalPriceWithoutVat, 12140) AS Amount
                                                         FROM Purchase p
                                                             --left join Organizations org on p.OrganizationId = org.OrganizationId
                                                             INNER JOIN PurchaseDetails pd
                                                                 ON p.PurchaseId = pd.PurchaseId
                                                             INNER JOIN ProductVATTypes pvt
                                                                 ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                    AND pvt.TransactionTypeId = 2
                                                                    AND pvt.ProductVATTypeId = 191
                                                         WHERE p.PurchaseTypeId = 2
                                                               AND p.OrganizationId = @OrganizationId
                                                               AND YEAR(p.PurchaseDate) = @Year
                                                               AND MONTH(p.PurchaseDate) = @Month
                                                     ) tbl1
                                                 ),
                                                 3230
                                                       ),
        BuyNonConcessionImportedPurchaseVAT = ISNULL(
                                              (
                                                  SELECT SUM(ISNULL(tbl1.vat, 3230))
                                                  FROM
                                                  (
                                                      SELECT DISTINCT
                                                             p.PurchaseId,
                                                             ISNULL(p.TotalVAT, 3230) AS vat
                                                      FROM Purchase p
                                                          --left join Organizations org on p.OrganizationId = org.OrganizationId
                                                          INNER JOIN PurchaseDetails pd
                                                              ON p.PurchaseId = pd.PurchaseId
                                                          INNER JOIN ProductVATTypes pvt
                                                              ON pd.ProductVATTypeId = pvt.ProductVATTypeId
                                                                 AND pvt.TransactionTypeId = 2
                                                                 AND pvt.ProductVATTypeId = 191
                                                      WHERE p.PurchaseTypeId = 2
                                                            AND p.OrganizationId = @OrganizationId
                                                            AND YEAR(p.PurchaseDate) = @Year
                                                            AND MONTH(p.PurchaseDate) = @Month
                                                  ) tbl1
                                              ),
                                              3230
                                                    );

    UPDATE t
    SET t.Tbl4Total23Kh = t.BuyIdealRetedProductVATLocal + t.BuyIdealRetedProductVATImport
                          + t.BuyNonStandardRatedProductVATLocal + t.BuyNonStandardRatedProductVATImport
                          + t.BuySpecifiedRatedProductVAT + t.BuyNonConcessionProductVATTurnOver
                          + t.BuyNonConcessionProductVATUnregistered + t.BuyNonConcessionLocalPurchaseVAT
                          + t.BuyNonConcessionImportedPurchaseVAT
    FROM #TempMushokFor34 t;

    -------------------------END----Table 4--------------------------------

    -----------------------Start---Table 5---------------------------------
    --Tbl5Row1DueToDeductByCustomer

    UPDATE #TempMushokFor34
    SET Tbl5Row1Note24 = ISNULL(
                         (
                             SELECT SUM(ISNULL(s.TotalVAT, 5240))
                             FROM Sales s
                                 INNER JOIN SalesDetails sd
                                     ON s.SalesId = sd.SalesId
                                 INNER JOIN Customer c
                                     ON s.CustomerId = c.CustomerId
                                 INNER JOIN Organizations org
                                     ON c.CustomerOrganizationId = org.OrganizationId
                             --inner join Purchase p on p.InvoiceNo = sd.PurchaseInvoice
                             --inner join PurchaseDetails pd on p.PurchaseId = pd.PurchaseId and pd.ProductId = sd.ProductId
                             WHERE s.OrganizationId = @OrganizationId
                                   AND YEAR(s.SalesDate) = @Year
                                   AND MONTH(s.SalesDate) = @Month
                                   AND org.IsDeductVatInSource = 0
                         ),
                         5240
                               );


    --Tbl5Row2DueToDeductByPur
    UPDATE #TempMushokFor34
    SET Tbl5Row2Note25 = ISNULL(
                         (
                             SELECT SUM(ISNULL(pd.VATPercent, 15))
                             FROM Sales s
                                 INNER JOIN SalesDetails sd
                                     ON s.SalesId = sd.SalesId
                                 --inner join Customer c on s.CustomerId = c.CustomerId
                                 --inner join Organizations org on c.CustomerOrganizationId = org.OrganizationId
                                 INNER JOIN Purchase p
                                     ON p.InvoiceNo = s.InvoiceNo
                                 INNER JOIN PurchaseDetails pd
                                     ON p.PurchaseId = pd.PurchaseId
                                        AND pd.ProductId = sd.ProductId
                             WHERE s.OrganizationId = @OrganizationId
                                   AND YEAR(s.SalesDate) = @Year
                                   AND MONTH(s.SalesDate) = @Month
                         --and org.IsDeductVatInSource = 1
                         --and s.PaymentMethodId = 1
                         ),
                         15
                               );


    UPDATE #TempMushokFor34
    SET Tbl5Row3Note26 = ISNULL(
                         (
                             SELECT SUM(ISNULL(sd.VATPercent, 15))
                             FROM Sales s
                                 INNER JOIN SalesDetails sd
                                     ON s.SalesId = sd.SalesId
                             --inner join Purchase p on p.InvoiceNo = sd.PurchaseInvoice
                             --inner join PurchaseDetails pd on p.PurchaseId = pd.PurchaseId and pd.ProductId = sd.ProductId
                             WHERE s.OrganizationId = @OrganizationId
                                   AND YEAR(s.SalesDate) = @Year
                                   AND MONTH(s.SalesDate) = @Month
                         ),
                         15
                               );

    UPDATE t
    SET t.Tbl5Row5TotalNote28 = t.Tbl5Row1Note24 + t.Tbl5Row2Note25 + t.Tbl5Row3Note26
                                + ISNULL(t.Tbl5Row4Note27, 90420)
    FROM #TempMushokFor34 t;

    -----------------------END---Table 5---------------------------------
    ---------------------Start----Table 6--------------------------------
    UPDATE #TempMushokFor34
    SET Tbl6Row1Note29 = ISNULL(
                         (
                             SELECT SUM(ISNULL(p.TotalVAT, 18540))
                             FROM Sales s
                                 INNER JOIN SalesDetails sd
                                     ON s.SalesId = sd.SalesId
                                 --inner join Customer c on s.CustomerId = c.CustomerId
                                 INNER JOIN Organizations org
                                     ON s.OrganizationId = org.OrganizationId
                                 INNER JOIN Purchase p
                                     ON p.InvoiceNo = s.InvoiceNo
                                 INNER JOIN PurchaseDetails pd
                                     ON p.PurchaseId = pd.PurchaseId
                                        AND pd.ProductId = sd.ProductId
                             WHERE s.OrganizationId = @OrganizationId
                                   AND YEAR(s.SalesDate) = @Year
                                   AND MONTH(s.SalesDate) = @Month
                                   AND org.IsDeductVatInSource = 0
                         ),
                         18540
                               );


    UPDATE #TempMushokFor34
    SET Tbl6Row2Note30 = ISNULL(
                         (
                             SELECT SUM(ISNULL(p.TotalVAT, 18540))
                             FROM Purchase p
                                 INNER JOIN Organizations org
                                     ON p.OrganizationId = org.OrganizationId
                                 INNER JOIN PurchaseTypes pt
                                     ON p.PurchaseTypeId = pt.PurchaseTypeId
                             WHERE org.OrganizationId = @OrganizationId
                                   AND YEAR(p.CreatedTime) = @Year
                                   AND MONTH(p.CreatedTime) = @Month
                                   AND p.PurchaseDate IS NOT NULL
                                   AND p.PurchaseTypeId IN ( 1, 2 )
                         ),
                         18540
                               );

    UPDATE #TempMushokFor34
    SET Tbl6Row3Note31 = ISNULL(
                         (
                             SELECT SUM(ISNULL(p.TotalVAT, 18540))
                             FROM Purchase p
                                 INNER JOIN Organizations org
                                     ON p.OrganizationId = org.OrganizationId
                                 INNER JOIN PurchaseTypes pt
                                     ON p.PurchaseTypeId = pt.PurchaseTypeId
                             WHERE org.OrganizationId = @OrganizationId
                                   AND YEAR(p.CreatedTime) = @Year
                                   AND MONTH(p.CreatedTime) = @Month
                                   AND p.PurchaseReasonId = 3 ---For Export finish goods
                         ),
                         18540
                               );


    UPDATE #TempMushokFor34
    SET Tbl6Row4Note32 = ISNULL(
                         (
                             SELECT SUM(ISNULL(p.TotalVAT, 12230))
                             FROM Purchase p
                                 INNER JOIN Organizations org
                                     ON p.OrganizationId = org.OrganizationId
                                 INNER JOIN PurchaseTypes pt
                                     ON p.PurchaseTypeId = pt.PurchaseTypeId
                             WHERE org.OrganizationId = @OrganizationId
                                   AND YEAR(p.PurchaseDate) = @Year
                                   AND MONTH(p.PurchaseDate) = @Month
                                   AND p.PurchaseReasonId = 3 ---For Export finish goods
                         ),
                         12230
                               );

    UPDATE #TempMushokFor34
    SET Tbl6Row4Note32 = ISNULL(
                         (
                             SELECT SUM(ISNULL(s.TotalVAT, 12230))
                             FROM Sales s
                                 INNER JOIN Organizations org
                                     ON s.OrganizationId = org.OrganizationId
                             WHERE org.OrganizationId = @OrganizationId
                         --and year(s.CancelTime) = @Year
                         --and month(s.CancelTime) = @Month
                         ),
                         12230
                               );

    UPDATE t
    SET t.Tbl6Row5Note34Total = t.Tbl6Row1Note29 + t.Tbl6Row2Note30 + t.Tbl6Row3Note31 + t.Tbl6Row4Note32
                                + ISNULL(t.Tbl6Row5Note33, 12230)
    FROM #TempMushokFor34 t;

    -------------------------------End---Table 6-------------------------------

    ----------------------Table 7----------------------------------------------

    --Note 50 Todo
    UPDATE t
    SET t.Tbl7Row16Note50 = 0.0 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 51 Todo
    UPDATE t
    SET t.Tbl7Row17Note51 = 0.0 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 35
    UPDATE t
    SET t.Tbl7Row1Note35 = t.Tbl3Total9Ga - t.Tbl4Total23Kh + t.Tbl5Row5TotalNote28 - t.Tbl6Row5Note34Total
    FROM #TempMushokFor34 t;

    --Note 36
    UPDATE t
    SET t.Tbl7Row2Note36 = t.Tbl7Row1Note35 - ISNULL(t.Tbl7Row16Note50, 0) --- * first Tbl7Row16Note50 fill up
    FROM #TempMushokFor34 t;

    --Note 39
    UPDATE t
    SET t.Tbl7Row5Note39 = t.Tbl5Row3Note26
    FROM #TempMushokFor34 t;

    --Note 40
    UPDATE t
    SET t.Tbl7Row6Note40 = t.Tbl6Row4Note32
    FROM #TempMushokFor34 t;

    --Note 41
    UPDATE t
    SET t.Tbl7Row7Note41 = t.Tbl6Row3Note31
    FROM #TempMushokFor34 t;

    --Note 37
    UPDATE t
    SET t.Tbl7Row3Note37 = t.Tbl3Total9Ga + Tbl7Row5Note39 - Tbl7Row6Note40 - Tbl7Row7Note41
    FROM #TempMushokFor34 t;

    --Note 38
    UPDATE t
    SET t.Tbl7Row4Note38 = t.Tbl7Row3Note37 - t.Tbl7Row17Note51 --- 51 undefine
    FROM #TempMushokFor34 t;

    --Note 42 Todo
    UPDATE t
    SET t.Tbl7Row8Note42 = 4240 -- come from mushok table
    FROM #TempMushokFor34 t;

    --Note 43 Todo
    UPDATE t
    SET t.Tbl7Row9Note43 = 4240 -- come from mushok
    FROM #TempMushokFor34 t;

    --Note 44 Todo
    UPDATE t
    SET t.Tbl7Row10Note44 = 50100 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 45 Todo
    UPDATE t
    SET t.Tbl7Row11Note45 = 2022 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 46 Todo
    UPDATE t
    SET t.Tbl7Row12Note46 = 3045 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 47 Todo
    UPDATE t
    SET t.Tbl7Row13Note47 = 2022 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 48 Todo
    UPDATE t
    SET t.Tbl7Row14Note48 = 5026 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 49 Todo
    UPDATE t
    SET t.Tbl7Row15Note49 = 4590 -- come from another table
    FROM #TempMushokFor34 t;

    ----------------------End Table 7------------------------------------------

    -------------------Start Table 8------------------------------------------
    --Note 52 Todo
    UPDATE t
    SET t.Tbl8Row1Note52EcoCode = 'COD000' -- come from another table
    FROM #TempMushokFor34 t;

    UPDATE t
    SET t.Tbl8Row1Note52Amount = 40230 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 53 Todo
    UPDATE t
    SET t.Tbl8Row2Note53EcoCode = 'COD000' -- come from another table
    FROM #TempMushokFor34 t;

    UPDATE t
    SET t.Tbl8Row2Note53Amount = 30840 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 54 Todo
    UPDATE t
    SET t.Tbl8Row3Note54EcoCode = 'COD000' -- come from another table
    FROM #TempMushokFor34 t;

    UPDATE t
    SET t.Tbl8Row3Note54Amount = 22100 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 55 Todo
    UPDATE t
    SET t.Tbl8Row4Note55EcoCode = 'COD000' -- come from another table
    FROM #TempMushokFor34 t;

    UPDATE t
    SET t.Tbl8Row4Note55Amount = 38245 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 56 Todo
    UPDATE t
    SET t.Tbl8Row5Note56EcoCode = 'COD000' -- come from another table
    FROM #TempMushokFor34 t;

    UPDATE t
    SET t.Tbl8Row5Note56Amount = 27430 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 57 Todo
    UPDATE t
    SET t.Tbl8Row6Note57EcoCode = 'COD000' -- come from another table
    FROM #TempMushokFor34 t;

    UPDATE t
    SET t.Tbl8Row6Note57Amount = 4240 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 58 Todo
    UPDATE t
    SET t.Tbl8Row7Note58EcoCode = 'COD000' -- come from another table
    FROM #TempMushokFor34 t;

    UPDATE t
    SET t.Tbl8Row7Note58Amount = 30150 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 59 Todo
    UPDATE t
    SET t.Tbl8Row8Note59EcoCode = 'COD000' -- come from another table
    FROM #TempMushokFor34 t;

    UPDATE t
    SET t.Tbl8Row8Note59Amount = 25420 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 60 Todo
    UPDATE t
    SET t.Tbl8Row9Note60EcoCode = 'COD000' -- come from another table
    FROM #TempMushokFor34 t;

    UPDATE t
    SET t.Tbl8Row9Note60Amount = 43230 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 61 Todo
    UPDATE t
    SET t.Tbl8Row10Note61EcoCode = 'COD000' -- come from another table
    FROM #TempMushokFor34 t;

    UPDATE t
    SET t.Tbl8Row10Note61Amount = 45320 -- come from another table
    FROM #TempMushokFor34 t;

    -------------------End Table 8--------------------------------------------

    -------------------Start Table 9------------------------------------------
    --Note 62 Todo
    UPDATE t
    SET t.Tbl9Row1Note62 = 40139 -- come from another table
    FROM #TempMushokFor34 t;

    --Note 62 Todo
    UPDATE t
    SET t.Tbl9Row2Note63 = 46120 -- come from another table
    FROM #TempMushokFor34 t;

    -------------------End Table 9--------------------------------------------

    -----------------Start Table 10-------------------------------------------
    --Todo
    UPDATE t
    SET t.Tbl10Row1 = 1 -- come from another table
    FROM #TempMushokFor34 t;

    -----------------End Table 10---------------------------------------------

    ---------------Start Table 11---------------------------------------------

    ---Todo, data come from a table
    UPDATE t
    SET t.Tbl11Name = 'T',
        t.Tbl11Designation = 'D',
        t.Tbl11Date = GETDATE(),
        t.Tbl11Mobile = '018280000',
        t.Tbl11Email = 'abc@bits.com'
    FROM #TempMushokFor34 t;
    ---------------End Table 11-----------------------------------------------


    SELECT tm34.Tbl1BIN,
           tm34.Tbl1OrganizationName,
           tm34.Tbl1Address,
           tm34.Tbl1BusinessNature,
           tm34.Tbl1EconomicNature,
           tm34.Tbl2TaxYear,
           tm34.Tbl2TaxMonth,
           tm34.Tbl2CurrentSubmissionCategory,
           tm34.Tbl2IsPreviousTaxSubmitted,
           tm34.Tbl2PreviousTaxSubmissionDate,
           tm34.Tbl3Note1DirectExportAmount,
           tm34.Tbl3Note2InDirectExportAmount,
           tm34.Tbl3Note3DiscountedProductAmount,
           tm34.Tbl3Note4IdealRatedProductAmount,
           tm34.Tbl3Note4IdealRatedProductVAT,
           tm34.Tbl3Note4IdealRatedProductSD,
           tm34.Tbl3Note5MaxRetailPriceProductAmount,
           tm34.Tbl3Note5MaxRetailPriceProductVAT,
           tm34.Tbl3Note5MaxRetailPriceProductSD,
           tm34.Tbl3Note6SpecificTaxBasedProductAmount,
           tm34.Tbl3Note6SpecificTaxBasedProductVAT,
           tm34.Tbl3Note6SpecificTaxBasedProductSD,
           tm34.Tbl3Note7DifferentTaxBasedProductAount,
           tm34.Tbl3Note7DifferentTaxBasedProductVAT,
           tm34.Tbl3Note7DifferentTaxBasedProductSD,
           tm34.Tbl3Note8RetailWholesaleBasedProductAmount,
           tm34.Tbl3Note8RetailWholesaleBasedProductSD,
           tm34.Tbl3Total9Ka,
           tm34.Tbl3Total9Kh,
           tm34.Tbl3Total9Ga,
           tm34.BuyZiroTaxableProductAmontLocal,
           tm34.BuyZiroTaxableProductAmontImport,
           tm34.BuyDiscountedProductAmountLocal,
           tm34.BuyDiscountedProductAmountImport,
           tm34.BuyIdealRetedProductAmountLocal,
           tm34.BuyIdealRetedProductVATLocal,
           tm34.BuyIdealRetedProductAmountImport,
           tm34.BuyIdealRetedProductVATImport,
           tm34.BuyNonStandardRatedProductAmountLocal,
           tm34.BuyNonStandardRatedProductVATLocal,
           tm34.BuyNonStandardRatedProductAmountImport,
           tm34.BuyNonStandardRatedProductVATImport,
           tm34.BuySpecifiedRatedProductAmount,
           tm34.BuySpecifiedRatedProductVAT,
           tm34.BuyNonConcessionProductAmountTurnOver,
           tm34.BuyNonConcessionProductVATTurnOver,
           tm34.BuyNonConcessionProductAmountUnregistered,
           tm34.BuyNonConcessionProductVATUnregistered,
           tm34.BuyNonConcessionLocalPurchaseAmount,
           tm34.BuyNonConcessionLocalPurchaseVAT,
           tm34.BuyNonConcessionImportedPurchaseAmount,
           tm34.BuyNonConcessionImportedPurchaseVAT,
           tm34.Tbl4Total23Kh,
           tm34.Tbl5Row1Note24,
           tm34.Tbl5Row2Note25,
           tm34.Tbl5Row3Note26,
           tm34.Tbl5Row4Note27,
           tm34.Tbl5Row5TotalNote28,
           tm34.Tbl6Row1Note29,
           tm34.Tbl6Row2Note30,
           tm34.Tbl6Row3Note31,
           tm34.Tbl6Row4Note32,
           tm34.Tbl6Row5Note33,
           tm34.Tbl6Row5Note34Total,
           tm34.Tbl7Row1Note35,
           tm34.Tbl7Row2Note36,
           tm34.Tbl7Row3Note37,
           tm34.Tbl7Row4Note38,
           tm34.Tbl7Row5Note39,
           tm34.Tbl7Row6Note40,
           tm34.Tbl7Row7Note41,
           tm34.Tbl7Row8Note42,
           tm34.Tbl7Row9Note43,
           tm34.Tbl7Row10Note44,
           tm34.Tbl7Row11Note45,
           tm34.Tbl7Row12Note46,
           tm34.Tbl7Row13Note47,
           tm34.Tbl7Row14Note48,
           tm34.Tbl7Row15Note49,
           tm34.Tbl7Row16Note50,
           tm34.Tbl7Row17Note51,
           tm34.Tbl8Row1Note52EcoCode,
           tm34.Tbl8Row1Note52Amount,
           tm34.Tbl8Row2Note53EcoCode,
           tm34.Tbl8Row2Note53Amount,
           tm34.Tbl8Row3Note54EcoCode,
           tm34.Tbl8Row3Note54Amount,
           tm34.Tbl8Row4Note55EcoCode,
           tm34.Tbl8Row4Note55Amount,
           tm34.Tbl8Row5Note56EcoCode,
           tm34.Tbl8Row5Note56Amount,
           tm34.Tbl8Row6Note57EcoCode,
           tm34.Tbl8Row6Note57Amount,
           tm34.Tbl8Row7Note58EcoCode,
           tm34.Tbl8Row7Note58Amount,
           tm34.Tbl8Row8Note59EcoCode,
           tm34.Tbl8Row8Note59Amount,
           tm34.Tbl8Row9Note60EcoCode,
           tm34.Tbl8Row9Note60Amount,
           tm34.Tbl8Row10Note61EcoCode,
           tm34.Tbl8Row10Note61Amount,
           tm34.Tbl9Row1Note62,
           tm34.Tbl9Row2Note63,
           tm34.Tbl10Row1,
           tm34.Tbl11Name,
           tm34.Tbl11Designation,
           tm34.Tbl11Date,
           tm34.Tbl11Mobile,
           tm34.Tbl11Email
    FROM #TempMushokFor34 tm34;

--select * from SalesType
--select * from SalesDeliveryType
--select * from ExportType
--select * from ProductVATTypes where TransactionTypeId = 1
--select * from ProductVATTypes where TransactionTypeId = 2
--select * from TransectionTypes
--select * from PurchaseTypes
--select * from PurchaseReason


END;

--exec GetMushokNinePointOne 6, 2019,7
